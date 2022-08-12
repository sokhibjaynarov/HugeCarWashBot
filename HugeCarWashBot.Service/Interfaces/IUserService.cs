using HugeCarWashBot.Domain.Commons;
using HugeCarWashBot.Domain.Configurations;
using HugeCarWashBot.Domain.Entities.Users;
using HugeCarWashBot.Service.DTOs.Users;
using System.Linq.Expressions;

namespace HugeCarWashBot.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<User>> CreateAsync(UserForCreationDto userDto);
        Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression);
        Task<BaseResponse<IEnumerable<User>>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression);
        Task<BaseResponse<User>> UpdateAsync(Guid id, UserForCreationDto userDto);
    }
}
