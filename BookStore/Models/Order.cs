namespace BookStore.Models
{
    public class Order : Entity
    {
        public User User { get; set; }
        public List<OrderQuantity> OrderQuantities { get; set; }
        public DateTime OrderDate { get; set; }

        public Order()
        {

        }
        public Order(User user)
        {
            User = user;
            OrderQuantities = new List<OrderQuantity>();
            OrderDate = DateTime.Now;
        }
    }
}
