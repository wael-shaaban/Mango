namespace MMango.Services.OrderAPI
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }//value represent threshold to apply discount amount if reach
    }
}
