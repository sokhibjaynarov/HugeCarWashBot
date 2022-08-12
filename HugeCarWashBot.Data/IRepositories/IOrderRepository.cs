using HugeCarWashBot.Domain.Entities.Orders;
using System.Linq.Expressions;

namespace HugeCarWashBot.Data.IRepositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order Order);
        Task<Order> UpdateAsync(Order Order);
        Task<Order> GetAsync(Expression<Func<Order, bool>> expression);
        Task<IQueryable<Order>> GetAllAsync(Expression<Func<Order, bool>> expression = null);
        Task<bool> DeleteAsync(Expression<Func<Order, bool>> expression);
    }
}
