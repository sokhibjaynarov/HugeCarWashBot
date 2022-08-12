namespace HugeCarWashBot.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IEmployeeRepository Employees { get; }
        IOrderRepository Orders { get; }
        IUserHelperRepository UserHelpers { get; }

        Task SaveChangesAsync();
    }
}
