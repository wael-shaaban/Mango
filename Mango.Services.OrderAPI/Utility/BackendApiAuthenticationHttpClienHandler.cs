
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace Mango.Services.ShoppingCartAPI.Utility
{
    public class BackendApiAuthenticationHttpClienHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");//static_name ==> access_token
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);//Static Scheme=> Bearer
            return await base.SendAsync(request, cancellationToken);
        }
    }
}