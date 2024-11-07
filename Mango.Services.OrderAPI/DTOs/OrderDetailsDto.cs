using Mango.Services.OrderAPI.DTOs;
using Mango.Services.ShoppingCartAPI.Models;
using System.Text.Json.Serialization;
namespace MMango.Services.OrderAPI
{
    public class OrderDetailsDto
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        //[JsonIgnore]
        public OrderHeaderDto? CartHeader { get; set; }
        public int ProductId { get; set; }
        //[JsonIgnore]
        public ProductDTO? Product { get; set; }
        public int Count { get; set; }
    }
}
