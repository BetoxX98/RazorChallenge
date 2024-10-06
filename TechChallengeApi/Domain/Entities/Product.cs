using Domain.Entities.Base;

namespace Domain.Entities
{
    public class Product : SoftDeleteBaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public Guid? TypeId { get; set; }
        public ProductType? Type { get; set; } = new ProductType();
    }
}
