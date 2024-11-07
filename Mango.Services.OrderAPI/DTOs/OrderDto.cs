namespace MMango.Services.OrderAPI
{
    public class OrderDto
    {
        public OrderDto()
        {
            CartDetails = new HashSet<OrderDetailsDto>();    
        }
        public OrderHeaderDto CartHeader { get; set; }
        public ICollection<OrderDetailsDto>? CartDetails { get; set; }
    }
}
