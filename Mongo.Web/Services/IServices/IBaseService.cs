using Mongo.Web.ViewModels;

namespace Mongo.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<GeneralResponseDTO?> SendAsync(RequestDTO requestDTO,bool withJwtBrearer = true);
    }
}
