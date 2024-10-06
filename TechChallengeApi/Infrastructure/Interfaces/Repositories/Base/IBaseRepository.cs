using Microsoft.EntityFrameworkCore;
using Resources.Dtos.Base;

namespace Infrastructure.Interfaces.Repositories.Base
{
    public interface IBaseRepository<TEntity, TEntityKey>
        where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(TEntityKey entityId);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllPaginatedAsync(int skip, int take);
        Task<SaveResultDto<TEntity>> CreateAndSaveAsync(TEntity entity);
        Task<SaveResultDto<TEntity>> UpdateAndSaveAsync(TEntity entities);
        Task<SaveResultDto<TEntity>> DeleteAndSaveAsync(TEntity entity);
    }
}
