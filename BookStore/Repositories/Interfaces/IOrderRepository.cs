using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersByUser(User user);
    }
}
