using DataAcces.Context;
using DataAcces.Repositories.Base;
using Domain.Entities;
using Infrastructure.Interfaces.Context;
using Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAcces.Repositories
{
    public class ProductTypeRepository : BaseRepository<ApiContext, ProductType, Guid>, IProductTypeRepository
    {
        public ProductTypeRepository(IApiContext apiContext, ILogger<BaseRepository<ApiContext, ProductType, Guid>> logger) : base(apiContext, logger)
        {
        }

        public override async Task<ProductType?> GetByIdAsync(Guid entityId)
        {
            return await Context.Set<ProductType>()
                                .Where(x => !x.IsDeleted)
                                .FirstOrDefaultAsync(x => x.Id == entityId);
        }

        public override async Task<IEnumerable<ProductType>> GetAllAsync()
        {
            return await Context.Set<ProductType>()
                                .Where(x => !x.IsDeleted)
                                .ToListAsync();
        }

        public override async Task<IEnumerable<ProductType>> GetAllPaginatedAsync(int skip, int take)
        {
            return await Context.Set<ProductType>()
                                .Where(x => !x.IsDeleted)
                                .Skip(skip).Take(take)
                                .ToListAsync();
        }
    }
}
