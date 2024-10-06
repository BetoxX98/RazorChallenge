using Dtos.Dtos.Response;

namespace Resources.Dtos.Response
{
    public class ResponseDto
    {
        public bool Success { get; set; }

        public IEnumerable<ErrorDto>? Errors { get; set; }
    }
}
