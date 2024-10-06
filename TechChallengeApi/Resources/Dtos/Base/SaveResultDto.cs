namespace Resources.Dtos.Base
{
    public class SaveResultDto
    {
        public bool IsOk { get; set; } = true;
        public SaveErrorDto? Error { get; set; }

        public bool HasError
        {
            get
            {
                return Error != null;
            }
        }
    }

    public class SaveResultDto<TEntity> : SaveResultDto
    {
        public SaveResultDto(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; set; }
    }
}
