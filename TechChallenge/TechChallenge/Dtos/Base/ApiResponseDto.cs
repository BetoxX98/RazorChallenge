namespace TechChallenge.Dtos.Base
{
    public class ApiResponseDto<TModel>
    {
        public TModel? Data { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
