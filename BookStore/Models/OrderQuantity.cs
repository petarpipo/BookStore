namespace BookStore.Models
{
    public class OrderQuantity : Entity
    {
        public int Quantity { get; set; }
        public Book Book { get; set; }
    }
}
