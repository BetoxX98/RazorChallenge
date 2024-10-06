using Common.Enums.Response;

namespace Dtos.Dtos.Response
{
    public class ErrorDto
    {
        public ErrorDto(ErrorTypeEnum _errorType, string _errorMessageKey)
        {
            errorType = _errorType;
            errorMessageKey = _errorMessageKey;
        }

        public ErrorTypeEnum errorType { get; set; }
        public string errorMessageKey { get; set; }
    }
}
