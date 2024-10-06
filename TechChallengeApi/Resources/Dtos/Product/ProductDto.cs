using Domain.Entities.Base;
using Resources.Dtos.ProductType;

namespace Resources.Dtos.Product
{
    public class ProductDto : SoftDeleteBaseAuditedEntityDto<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public ProductTypeDto? Type { get; set; } = new ProductTypeDto();
    }
}
