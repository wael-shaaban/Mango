namespace Mango.Services.AuthAPI.DTOs
{
    public class GeneralResponseDto
    {
        public bool Success { get; set; } = true;
        public string? Message { get; set; }=string.Empty;
        public dynamic? Data { get; set; } = null;
    }
}
