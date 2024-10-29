namespace Mongo.Web.ViewModels
{
    public record LoginResponseDto(UserDto User, string Token);
}