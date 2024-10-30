namespace Mongo.Web.Services.IServices
{
    public interface ITokeProvider
    {
        void SetToken(string token);
        string? GetToken();
        void ClearToken();
    }
}
