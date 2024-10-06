using Infrastructure.Interfaces.Repositories.Base;
using Domain.Entities;

namespace Infrastructure.Interfaces.Repositories
{
    public interface IProductTypeRepository : IBaseRepository<ProductType, Guid>
    {
    }
}
