using System.Text.Json.Serialization;
using TechChallenge.Dtos.Base;

namespace TechChallenge.Dtos.ProductType
{
    public class ProductTypeDto : SoftDeleteBaseAuditedEntityDto<Guid>
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("color")]
        public string Color { get; set; } = string.Empty;
    }
}
