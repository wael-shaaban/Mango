using System.ComponentModel.DataAnnotations;

namespace Mongo.Web.ViewModels
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Discount Amount must be a positive number.")]
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
