using Mango.Services.EmailApi.Dtos;

namespace Mango.Services.EmailApi.Services.IServices
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
    }
}
