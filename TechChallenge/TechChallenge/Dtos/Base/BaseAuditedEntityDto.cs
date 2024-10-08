using System.Text.Json.Serialization;

namespace TechChallenge.Dtos.Base
{
    public class BaseAuditedEntityDto<T>
    {
        [JsonPropertyName("id")]
        public T Id { get; set; }
        [JsonPropertyName("creationDate")]
        public DateTime? CreationDate { get; set; }
        [JsonPropertyName("creationUser")]
        public string? CreationUser { get; set; }
        [JsonPropertyName("updateDate")]
        public DateTime? UpdateDate { get; set; }
        [JsonPropertyName("updateUser")]
        public string? UpdateUser { get; set; }
    }
}
