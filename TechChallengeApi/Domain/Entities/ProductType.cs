using Domain.Entities.Base;

namespace Domain.Entities
{
    public class ProductType : SoftDeleteBaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
