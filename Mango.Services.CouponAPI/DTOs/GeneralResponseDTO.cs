namespace Mango.Services.CouponAPI.DTOs
{
    //public class GeneralResponseDTO<T>
    //{
    //    public bool Success { get; set; }
    //    public string Message { get; set; }
    //    public T Data { get; set; }
    //    public GeneralResponseDTO(bool success=true, string message="", T data=default)
    //    {
    //        Success = success;
    //        Message = message;
    //        Data = data;
    //    }
    //}
    public class GeneralResponseDTO
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public dynamic? Data { get; set; }
    }
}
