namespace Mongo.Web.ViewModels
{
    public class CartDto
    {
        public CartDto()
        {
            CartDetails = new HashSet<CartDetailsDto>();
        }
        public CartHeaderDto CartHeader { get; set; }
        public ICollection<CartDetailsDto>? CartDetails { get; set; }
    }
}
