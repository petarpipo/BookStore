namespace BookStore.Models.Responses
{
    public class OrderQuantityResponse
    {
        public int Quantity { get; set; }
        public OrderBookResponse Book { get; set; }
    }
}
