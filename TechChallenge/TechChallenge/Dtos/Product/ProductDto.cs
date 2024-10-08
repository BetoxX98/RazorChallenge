using System.Text.Json.Serialization;
using TechChallenge.Dtos.Base;
using TechChallenge.Dtos.ProductType;

namespace TechChallenge.Dtos.Product
{
    public class ProductDto : SoftDeleteBaseAuditedEntityDto<Guid>
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("price")]
        public double Price { get; set; }
        [JsonPropertyName("type")]
        public ProductTypeDto? Type { get; set; } = new ProductTypeDto();
    }
}
