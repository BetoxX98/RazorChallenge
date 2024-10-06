using System.Runtime.CompilerServices;
using DataAcces.Context;
using DataAcces.Repositories.Base;
using Domain.Entities;
using Infrastructure.Interfaces.Context;
using Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.Dtos.Base;

namespace DataAcces.Repositories
{
    public class ProductRepository : BaseRepository<ApiContext, Product, Guid>, IProductRepository
    {
        public ProductRepository(IApiContext apiContext, ILogger<BaseRepository<ApiContext, Product, Guid>> logger) : base(apiContext, logger)
        {
        }

        public override async Task<Product?> GetByIdAsync(Guid entityId)
        {
            return await Context.Set<Product>()
                                .Where(x => !x.IsDeleted)
                                .Include(x => x.Type)
                                .FirstOrDefaultAsync(x => x.Id == entityId);
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Context.Set<Product>()
                                .Where(x => !x.IsDeleted)
                                .Include(x => x.Type)
                                .ToListAsync();
        }

        public override async Task<IEnumerable<Product>> GetAllPaginatedAsync(int skip, int take)
        {
            return await Context.Set<Product>()
                                .Where(x => !x.IsDeleted)
                                .Skip(skip).Take(take)
                                .Include(x => x.Type)
                                .ToListAsync();
        }
    }
}
