using HugeCarWashBot.Data.Contexts;
using HugeCarWashBot.Data.IRepositories;
using HugeCarWashBot.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HugeCarWashBot.Data.Repositories
{
    public class UserHelperRepository : IUserHelperRepository
    {
        internal AppDbContext appDbContext;
        public UserHelperRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<UserHelper> CreateAsync(UserHelper UserHelper)
        {
            var entry = await appDbContext.UserHelpers.AddAsync(UserHelper);

            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<UserHelper, bool>> expression)
        {
            var entity = await appDbContext.UserHelpers.FirstOrDefaultAsync(expression);

            if (entity is null)
                return false;

            appDbContext.UserHelpers.Remove(entity);

            return true;
        }

        public async Task<IQueryable<UserHelper>> GetAllAsync(Expression<Func<UserHelper, bool>> expression = null)
        {
            return expression is null ? appDbContext.UserHelpers : appDbContext.UserHelpers.Where(expression);
        }

        public async Task<UserHelper> GetAsync(Expression<Func<UserHelper, bool>> expression)
        {
            var entity = await appDbContext.UserHelpers.FirstOrDefaultAsync(expression);
            return entity;
        }

        public async Task<UserHelper> UpdateAsync(UserHelper UserHelper)
        {
            var entry = appDbContext.UserHelpers.Update(UserHelper);

            return entry.Entity;
        }
    }
}
