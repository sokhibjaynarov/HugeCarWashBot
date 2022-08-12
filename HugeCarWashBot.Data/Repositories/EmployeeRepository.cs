using HugeCarWashBot.Data.Contexts;
using HugeCarWashBot.Data.IRepositories;
using HugeCarWashBot.Domain.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HugeCarWashBot.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        internal AppDbContext appDbContext;
        public EmployeeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Employee> CreateAsync(Employee Employee)
        {
            var entry = await appDbContext.Employees.AddAsync(Employee);

            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Employee, bool>> expression)
        {
            var entity = await appDbContext.Employees.FirstOrDefaultAsync(expression);

            if (entity is null)
                return false;

            appDbContext.Employees.Remove(entity);

            return true;
        }

        public async Task<IQueryable<Employee>> GetAllAsync(Expression<Func<Employee, bool>> expression = null)
        {
            return expression is null ? appDbContext.Employees : appDbContext.Employees.Where(expression);
        }

        public async Task<Employee> GetAsync(Expression<Func<Employee, bool>> expression)
        {
            var entity = await appDbContext.Employees.FirstOrDefaultAsync(expression);
            return entity;
        }

        public async Task<Employee> UpdateAsync(Employee Employee)
        {
            var entry = appDbContext.Employees.Update(Employee);

            return entry.Entity;
        }
    }
}
