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
    }
}
