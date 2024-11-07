using Mango.Services.OrderAPI.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.OrderAPI.Models
{
    public class OrderDetailsModel
    {
        [Key]
        public int OrderDetailsId { get; set; }
        public int OrderHeaderId { get; set; }
        [ForeignKey(nameof(OrderHeaderId))]
        public OrderHeaderModel? OrderHeader { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public ProductDTO? Product { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
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