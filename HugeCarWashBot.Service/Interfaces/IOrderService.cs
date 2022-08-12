using HugeCarWashBot.Domain.Commons;
using HugeCarWashBot.Domain.Configurations;
using HugeCarWashBot.Domain.Entities.Orders;
using HugeCarWashBot.Service.DTOs.Orders;
using System.Linq.Expressions;

namespace HugeCarWashBot.Service.Interfaces
{
    public interface IOrderService
    {
        Task<BaseResponse<Order>> CreateAsync(OrderForCreationDto OrderDto);
        Task<BaseResponse<Order>> GetAsync(Expression<Func<Order, bool>> expression);
        Task<BaseResponse<IEnumerable<Order>>> GetAllAsync(PaginationParams @params, Expression<Func<Order, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Order, bool>> expression);
        Task<BaseResponse<Order>> UpdateAsync(Guid id, OrderForCreationDto OrderDto);
        Task<BaseResponse<IEnumerable<OrderDto>>> GetAllOrdersAsync();
    }
}
