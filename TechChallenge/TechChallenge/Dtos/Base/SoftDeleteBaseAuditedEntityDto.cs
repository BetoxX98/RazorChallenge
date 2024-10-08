using System.Text.Json.Serialization;

namespace TechChallenge.Dtos.Base
{
    public class SoftDeleteBaseAuditedEntityDto<T> : BaseAuditedEntityDto<T>
    {
        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
