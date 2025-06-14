namespace BookStore.Models.Responses
{
    public class OrderResponse
    {
        public List<OrderQuantityResponse> OrderQuantities { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
