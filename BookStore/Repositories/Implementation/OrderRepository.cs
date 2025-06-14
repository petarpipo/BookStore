using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class OrderRepository : Repository<Order>,IOrderRepository
    {
        public OrderRepository(BookStoreContext context) : base(context)
        {
        }

        public async Task<List<Order>> GetOrdersByUser(User user)
        {
            return await _context.Order.Include(o => o.OrderQuantities).ThenInclude(q => q.Book)
                .Include(o => o.User).Where(o => o.User.Id == user.Id).ToListAsync();
        }
    }
}
