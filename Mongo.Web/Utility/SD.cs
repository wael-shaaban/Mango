namespace Mongo.Web.Utility
{
    public class SD
    {
        public static string CouponApiBaseUrl { get; set; }
        public static string AuthApiBaseUrl { get; set; }
        public static string ProductApiBaseUrl { get; set; }
        public static string ShoopingCartApiUrl { get; set; }

        public static string RoleAdmin = "Admin";
        public static string RoleCustomer = "Customer";
        public static string TokenCookie = "JwtCookie";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}