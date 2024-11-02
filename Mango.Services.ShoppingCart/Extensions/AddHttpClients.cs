using Polly.Extensions.Http;
using Polly;
using System.Net;
using Mango.Services.ShoppingCartAPI.Utility;

namespace Mango.Services.ShoppingCartAPI.Extensions
{
    public static class AddHttpClients
    {
        public static WebApplicationBuilder AddCustomhttpClient(this WebApplicationBuilder builder, string url, string? client = default)
        {
            builder.Services.AddHttpClient(client, options =>
            {
                options.BaseAddress = new Uri(url);
            }).AddHttpMessageHandler<BackendApiAuthenticationHttpClienHandler>()
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
                }).AddPolicyHandler(HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromMinutes(Math.Pow(1, retryAttempt)))) // Retry policy
                .AddPolicyHandler(HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30)))
                .AddPolicyHandler(Policy<HttpResponseMessage>
                .Handle<Exception>()
                .FallbackAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Service is currently unavailable.") }));
            return builder;
        }
    }
}