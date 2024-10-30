using Mongo.Web.Services.IServices;
using Mongo.Web.Utility;
using NuGet.Common;

namespace Mongo.Web.Services
{
    public class TokeProvider(IHttpContextAccessor httpContextAccessor) : ITokeProvider
    {
        public void ClearToken() =>
            httpContextAccessor.HttpContext.Response.Cookies.Delete(SD.TokenCookie);
        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);
            return hasToken is true ? token : null;
        }
        public void SetToken(string token) =>
            httpContextAccessor.HttpContext.Response.Cookies.Append(SD.TokenCookie, token);
    }
}