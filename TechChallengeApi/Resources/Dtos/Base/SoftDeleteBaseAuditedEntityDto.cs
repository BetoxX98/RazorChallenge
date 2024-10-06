namespace Domain.Entities.Base
{
    public class SoftDeleteBaseAuditedEntityDto<T> : BaseAuditedEntityDto<T>
    {
        public bool IsDeleted { get; set; }
    }
}
