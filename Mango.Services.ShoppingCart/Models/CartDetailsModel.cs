using Mango.Services.ShoppingCartAPI.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartAPI.Models
{
    public class CartDetailsModel
    {
        [Key]
        public int CartDetailsId { get; set; }
        [ForeignKey(nameof(CartHeader))]
        public int CartHeaderId { get; set; }
        public CartHeaderModel? CartHeader { get; set; }
        //[ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        [NotMapped]
        public ProductDTO? Product { get; set; }
        public int Count { get; set; }
    }
}