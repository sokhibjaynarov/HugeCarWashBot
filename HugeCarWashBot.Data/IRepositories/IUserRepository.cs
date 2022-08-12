using HugeCarWashBot.Domain.Entities.Users;
using System.Linq.Expressions;

namespace HugeCarWashBot.Data.IRepositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User> GetAsync(Expression<Func<User, bool>> expression);
        Task<IQueryable<User>> GetAllAsync(Expression<Func<User, bool>> expression = null);
        Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
    }
}
