using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore
{
    public class BookStoreContext : IdentityDbContext<User, Role, string>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderQuantity> OrderQuantity { get; set; }
    }
}
