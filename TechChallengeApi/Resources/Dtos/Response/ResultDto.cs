using Common.Enums.Response;
using Common.Utils.Extensions;
using Dtos.Dtos.Response;

namespace Resources.Dtos.Response
{
    public class ResultDto<TDto>
    {
        public ResultDto() { }

        public ResultDto(TDto? Dto)
        {
            Data = Dto;
        }

        public ResultDto(TDto? Dto, ErrorDto? error)
        {
            Data = Dto;
            AddError(error);
        }

        public ResultDto(TDto? Dto, ErrorTypeEnum errorType, string errorMessageKey)
        {
            Data = Dto;
            AddError(errorType, errorMessageKey);
        }

        public ResultDto(TDto? Dto, List<ErrorDto>? errors)
        {
            Data = Dto;
            if (errors != null) AddRangeError(errors);
        }

        public TDto? Data { get; private set; }

        public List<ErrorDto>? Errors { get; private set; }



        public void AddError(ErrorDto? error)
        {
            if (error is not null)
            {
                CheckErrorList();
                Errors.Add(error);
            }
        }

        public void AddError(ErrorTypeEnum errorType, string errorMessageKey)
        {
            AddError(new ErrorDto(errorType, errorMessageKey));
        }

        public void AddError(ErrorTypeEnum errorType)
        {
            AddError(new ErrorDto(errorType, errorType.ToString()));
        }

        public void AddRangeError(List<ErrorDto> errors)
        {
            if (errors is not null)
            {
                CheckErrorList();
                Errors.AddRange(errors);
            }
        }

        private void CheckErrorList()
        {
            if (Errors is null)
            {
                Errors = new();
            }
        }

        public bool HasError
        {
            get
            {
                return Errors.NotNullOrEmpty();
            }
        }

        public bool HasData
        {
            get
            {
                return Data is not null;
            }
        }

        public bool HasDataAndNoErrors
        {
            get
            {
                return !HasError && HasData;
            }
        }
    }
}
