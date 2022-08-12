using HugeCarWashBot.Domain.Entities.Employees;
using HugeCarWashBot.Domain.Entities.Orders;
using HugeCarWashBot.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace HugeCarWashBot.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<UserHelper> UserHelpers { get; set; }
    }
}
