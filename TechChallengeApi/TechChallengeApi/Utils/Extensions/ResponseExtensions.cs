using System.Net;
using Microsoft.AspNetCore.Mvc;
using Resources.Dtos.Response;

namespace TechChallengeApi.Utils.Extensions
{
    public static class ResponseExtensions
    {
        public static IActionResult ToHttpResponse(this ResponseDto response)
        {
            var status = !response.Success ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult ToHttpResponse<TModel>(this SingleResponseDto<TModel> response)
        {
            var status = HttpStatusCode.OK;

            if (!response.Success)
            {
                status = HttpStatusCode.InternalServerError;
            }
            else if (response.Data == null)
            {
                status = HttpStatusCode.NotFound;
            }

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }
    }

}
