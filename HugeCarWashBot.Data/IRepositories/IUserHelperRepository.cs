using HugeCarWashBot.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HugeCarWashBot.Data.IRepositories
{
    public interface IUserHelperRepository
    {
        Task<UserHelper> CreateAsync(UserHelper userHelper);
        Task<UserHelper> UpdateAsync(UserHelper userHelper);
        Task<UserHelper> GetAsync(Expression<Func<UserHelper, bool>> expression);
        Task<IQueryable<UserHelper>> GetAllAsync(Expression<Func<UserHelper, bool>> expression = null);
        Task<bool> DeleteAsync(Expression<Func<UserHelper, bool>> expression);
    }
}
