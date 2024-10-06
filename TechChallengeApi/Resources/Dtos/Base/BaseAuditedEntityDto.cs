namespace Domain.Entities.Base
{
    public class BaseAuditedEntityDto<T>
    {
        public T Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? CreationUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateUser { get; set; }
    }
}
