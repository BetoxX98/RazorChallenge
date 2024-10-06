namespace Domain.Entities.Base
{
    public class SoftDeleteBaseEntity<T> : BaseEntity<T>
    {
        public bool IsDeleted { get; set; }
    }
}
