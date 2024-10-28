using static Mongo.Web.Utility.SD;

namespace Mongo.Web.ViewModels
{
    public class RequestDTO
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string AccessToken {  get; set; }    
        public string Url {  get; set; }
        public dynamic? Data { get; set; }    
    }
}
