using Common.Constants;
using Common.Enums.Response;
using Dtos.Dtos.Response;
using Infrastructure.Interfaces.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Resources.Dtos.Response;
using TechChallengeApi.Utils.Extensions;

namespace TechChallengeApi.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> Logger;
        protected readonly IUser UserLogged;
        protected readonly IHttpContextAccessor Accessor;

        protected BaseController(ILogger<BaseController> logger, IUser user, IHttpContextAccessor accessor)
        {
            Logger = logger;
            UserLogged = user;
            Accessor = accessor;
        }

        protected string GetApplicationHeader()
        {
            string application = string.Empty;

            if (Accessor.HttpContext is not null && Accessor.HttpContext.Request.Headers.ContainsKey(GenericSettings.HeaderApplication))
            {
                application = Accessor.HttpContext.Request.Headers[GenericSettings.HeaderApplication].ToString();
            }

            return application;
        }

        protected string GetIp()
        {
            return HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
        }

        protected new IActionResult Response<TRes>(ResultDto<TRes>? result = null)
        {
            var response = new SingleResponseDto<TRes?>
            {
                Success = result?.Errors == null || !result.Errors.Any(),
                Data = result != null ? result.Data : default,
                Errors = (result?.Errors != null && result.Errors.Any()) ? result.Errors : new List<ErrorDto>()
            };

            try
            {
                Logger.LogDebug("Endpoint response: {@Response}", result);
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Error on serialize result");
            }

            return response.ToHttpResponse();
        }

        protected new IActionResult Response(object? result = null)
        {
            var errorsDtos = new List<ErrorDto>();
            var erros = ModelState.Values.SelectMany(v => v.Errors);

            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                errorsDtos.Add(new ErrorDto(ErrorTypeEnum.ValidationError, erroMsg));
            }

            var response = new SingleResponseDto<object?>()
            {
                Success = true,
                Data = result,
                Errors = errorsDtos
            };

            try
            {
                Logger.LogDebug("Endpoint response: {@Response}", result);
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Error on serialize result");
            }

            return response.ToHttpResponse();
        }
    }
}
