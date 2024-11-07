namespace Mango.Gateway2
{
    public class CustomHttpClientHandler : HttpClientHandler
    {
        public CustomHttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
        }
    }
}
