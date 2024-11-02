using Mango.Services.ShoppingCartAPI.Models;
using System.Text.Json.Serialization;
namespace Mango.Services.ShoppingCartAPI.DTOs
{
    public class CartDetailsDto
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        //[JsonIgnore]
        public CartHeaderDto? CartHeader { get; set; }
        public int ProductId { get; set; }
        //[JsonIgnore]
        public ProductDTO? Product { get; set; }
        public int Count { get; set; }
    }
}
