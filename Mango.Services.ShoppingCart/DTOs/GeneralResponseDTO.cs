namespace Mango.Services.ShoppingCartAPI.DTOs
{
    public class GeneralResponseDTO
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public dynamic? Data { get; set; }
    }
}
