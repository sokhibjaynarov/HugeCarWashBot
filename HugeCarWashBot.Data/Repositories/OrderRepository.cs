using HugeCarWashBot.Data.Contexts;
using HugeCarWashBot.Data.IRepositories;
using HugeCarWashBot.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HugeCarWashBot.Data.Repositories
{
#pragma warning disable
    public class OrderRepository : IOrderRepository
    {
        internal AppDbContext appDbContext;
        public OrderRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Order> CreateAsync(Order Order)
        {
            var entry = await appDbContext.Orders.AddAsync(Order);

            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Order, bool>> expression)
        {
            var entity = await appDbContext.Orders.FirstOrDefaultAsync(expression);

            if (entity is null)
                return false;

            appDbContext.Orders.Remove(entity);

            return true;
        }

        public async Task<IQueryable<Order>> GetAllAsync(Expression<Func<Order, bool>> expression = null)
        {
            return expression is null ? appDbContext.Orders : appDbContext.Orders.Where(expression);
        }

        public async Task<Order> GetAsync(Expression<Func<Order, bool>> expression)
        {
            var entity = await appDbContext.Orders.FirstOrDefaultAsync(expression);
            return entity;
        }

        public async Task<Order> UpdateAsync(Order Order)
        {
            var entry = appDbContext.Orders.Update(Order);

            return entry.Entity;
        }
    }
}
