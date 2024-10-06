using Common.Enums.Response;
using Infrastructure.Interfaces.Context;
using Infrastructure.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Resources.Dtos.Base;

namespace DataAcces.Repositories.Base
{
    public class BaseRepository<TApiContext, TEntity, TEntityKey> : IBaseRepository<TEntity, TEntityKey>
        where TEntity : class
        where TApiContext : DbContext
    {

        private readonly IApiContext _apiContext;
        protected readonly ILogger<BaseRepository<TApiContext, TEntity, TEntityKey>> _logger;

        public TApiContext Context
        {
            get
            {
                return (TApiContext)_apiContext;
            }
        }

        public DbSet<TEntity> DbSet
        {
            get
            {
                return Context.Set<TEntity>();
            }
        }

        public BaseRepository(IApiContext apiContext, ILogger<BaseRepository<TApiContext, TEntity, TEntityKey>> logger)
        {
            _apiContext = apiContext ?? throw new ArgumentException("ApiContext not set");
            _logger = logger ?? throw new ArgumentException("logger not set");

        }

        public async virtual Task<TEntity?> GetByIdAsync(TEntityKey entityId)
        {
            return await Context.Set<TEntity>().FindAsync(entityId);
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync(bool isTraceable = false)
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async virtual Task<SaveResultDto<TEntity>> CreateAndSaveAsync(TEntity entity)
        {
            await CreateAsync(entity);
            return await SaveChangeAsync(entity);
        }

        public async virtual Task<TEntity> CreateAsync(TEntity TEntity)
        {
            EntityEntry<TEntity> result = await DbSet.AddAsync(TEntity);
            return result.Entity;
        }

        public async virtual Task<SaveResultDto<TEntity>> UpdateAndSaveAsync(TEntity entity)
        {
            Update(entity);
            SaveResultDto<TEntity> result = await SaveChangeAsync(entity);
            if (result.IsOk)
            {
                result.Entity = entity;
            }
            return result;
        }

        public virtual TEntity Update(TEntity TEntity)
        {
            EntityEntry<TEntity> entry = Context.Entry(TEntity);

            EntityEntry<TEntity> result = DbSet.Attach(TEntity);
            entry.State = EntityState.Modified;

            return result.Entity;
        }

        public async virtual Task<SaveResultDto<TEntity>> DeleteAndSaveAsync(TEntity entity)
        {
            Delete(entity);
            SaveResultDto<TEntity> result = await SaveChangeAsync(entity);
            if (result.IsOk)
            {
                result.Entity = entity;
            }
            return result;
        }

        private void Delete(TEntity TEntity)
        {
            EntityEntry<TEntity> entry = Context.Entry(TEntity);

            DbSet.Remove(TEntity);
            entry.State = EntityState.Deleted;
        }

        public async Task<SaveResultDto<TEntity>> SaveChangeAsync(TEntity entity)
        {
            var result = new SaveResultDto<TEntity>(entity);
            try
            {
                int resultSave = await Context.SaveChangesAsync(true);
                if (resultSave <= 0)
                {
                    result.IsOk = false;
                    result.Error = SetErrorMessage("Not change has persisted.", ErrorTypeEnum.SaveChangesNoRows);
                    try { _logger.LogError(System.Text.Json.JsonSerializer.Serialize(result)); } catch (Exception ex) { }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                result.IsOk = false;
                result.Error = SetErrorMessage($"Concurrency Exception in SaveChanges: {ex.Message}", ErrorTypeEnum.SaveChangesConcurrencyException);
                try { DetachAllEntries(); _logger.LogError(ex.ToString()); _logger.LogError(System.Text.Json.JsonSerializer.Serialize(result)); } catch (Exception sex) { }
            }
            catch (Exception ex)
            {
                result.IsOk = false;
                result.Error = SetErrorMessage($"Exception in SaveChanges: {ex.Message}", ErrorTypeEnum.SaveChangesException);
                try { DetachAllEntries(); _logger.LogError(ex.ToString()); _logger.LogError(System.Text.Json.JsonSerializer.Serialize(result)); } catch (Exception sex) { }
            }

            return result;
        }

        public void DetachAllEntries()
        {
            foreach (var entry in Context.ChangeTracker.Entries().ToList())
            {
                Context.Entry(entry.Entity).State = EntityState.Detached;
            }
        }

        private SaveErrorDto SetErrorMessage(string errorMessage, ErrorTypeEnum errorType)
        {
            return new SaveErrorDto
            {
                ErrorMessage = errorMessage,
                ErrorType = errorType,
            };
        }
    }
}
