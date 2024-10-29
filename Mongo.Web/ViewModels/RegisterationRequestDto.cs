namespace Mongo.Web.ViewModels
{
    public record RegisterationRequestDto(string Name, string Email, string PhoneNumber, string Password, string Address, string? Role);
}