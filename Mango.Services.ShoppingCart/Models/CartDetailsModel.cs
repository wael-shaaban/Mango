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
/*{
  "cartHeader": {
    "cartHeaderId": 0,
    "userId": "11111111111111111",
    "couponCode": "",
    "discount": 0,
    "cartTotal": 122
  },
  "cartDetails": [
    {
      "cartDetailsId": 0,
      "cartHeaderId": 0,
      "productId": 3,
      "count": 33
    }
  ]
}*/