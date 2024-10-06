using Common.Enums.Response;

namespace Resources.Dtos.Base
{
    public class SaveErrorDto
    {
        public ErrorTypeEnum ErrorType { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
