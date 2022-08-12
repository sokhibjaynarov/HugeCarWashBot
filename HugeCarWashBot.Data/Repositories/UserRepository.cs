using HugeCarWashBot.Data.Contexts;
using HugeCarWashBot.Data.IRepositories;
using HugeCarWashBot.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HugeCarWashBot.Data.Repositories
{
#pragma warning disable
    public class UserRepository : IUserRepository
    {
        internal AppDbContext appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            var entry = await appDbContext.Users.AddAsync(user);

            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var entity = await appDbContext.Users.FirstOrDefaultAsync(expression);

            if (entity is null)
                return false;

            appDbContext.Users.Remove(entity);

            return true;
        }

        public async Task<IQueryable<User>> GetAllAsync(Expression<Func<User, bool>> expression = null)
        {
            return expression is null ? appDbContext.Users.Include(prop => prop.Orders) : appDbContext.Users.Include(prop => prop.Orders).Where(expression);
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            var entity = await appDbContext.Users.FirstOrDefaultAsync(expression);
            return entity;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var entry = appDbContext.Users.Update(user);

            return entry.Entity;
        }
    }
}
