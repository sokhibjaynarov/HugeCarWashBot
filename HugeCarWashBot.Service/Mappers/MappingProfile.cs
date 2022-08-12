using AutoMapper;
using HugeCarWashBot.Domain.Entities.Employees;
using HugeCarWashBot.Domain.Entities.Orders;
using HugeCarWashBot.Domain.Entities.Users;
using HugeCarWashBot.Service.DTOs.Employees;
using HugeCarWashBot.Service.DTOs.Orders;
using HugeCarWashBot.Service.DTOs.Users;

namespace HugeCarWashBot.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserForCreationDto>().ReverseMap();
            CreateMap<Order, OrderMapDto>().ReverseMap();
            CreateMap<Employee, EmployeeForCreationDto>().ReverseMap();
        }
    }
}
