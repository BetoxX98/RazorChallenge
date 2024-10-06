using Domain.Entities.Base;

namespace Resources.Dtos.ProductType
{
    public class ProductTypeDto : SoftDeleteBaseAuditedEntityDto<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}
