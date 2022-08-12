using HugeCarWashBot.Data.IRepositories;
using HugeCarWashBot.Data.Repositories;
using HugeCarWashBot.Service.Interfaces;
using HugeCarWashBot.Service.Services;

namespace HugeCarService.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IEmployeeService, EmployeeService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IUpdateHandler, UpdateHandler>()
                .AddScoped<IBotService, BotService>();
        }
    }
}
