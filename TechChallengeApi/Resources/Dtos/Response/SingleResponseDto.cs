namespace Resources.Dtos.Response
{
    public class SingleResponseDto<TModel> : ResponseDto
    {
        public TModel? Data { get; set; }
    }
}
