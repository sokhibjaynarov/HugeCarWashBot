using HugeCarWashBot.Domain.Entities.Employees;
using System.Linq.Expressions;

namespace HugeCarWashBot.Data.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task<Employee> GetAsync(Expression<Func<Employee, bool>> expression);
        Task<IQueryable<Employee>> GetAllAsync(Expression<Func<Employee, bool>> expression = null);
        Task<bool> DeleteAsync(Expression<Func<Employee, bool>> expression);
    }
}
