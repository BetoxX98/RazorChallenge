using Common.Constants;
using Infrastructure.Interfaces.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities
{
    public class User : IUser
    {
        private readonly IHttpContextAccessor Accessor;

        public User(IHttpContextAccessor accessor)
        {
            Accessor = accessor;
        }

        public string Id
        {
            get
            {
                string id = string.Empty;

                if (IsAuthenticated())
                {
                    id = Accessor.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == GenericSettings.UserId).Value;
                }

                return id;
            }
        }

        public string Name
        {
            get
            {
                string id = string.Empty;

                if (IsAuthenticated())
                {
                    id = Accessor.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == GenericSettings.UserName).Value;
                }

                return id;
            }
        }

        public bool IsAuthenticated()
        {
            return Accessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }


        public string GetApplicationHeader()
        {
            string application = string.Empty;

            if (Accessor.HttpContext is not null && Accessor.HttpContext.Request.Headers.ContainsKey(GenericSettings.HeaderApplication))
            {
                application = Accessor.HttpContext.Request.Headers[GenericSettings.HeaderApplication].ToString();
            }

            return application;
        }
    }
}
