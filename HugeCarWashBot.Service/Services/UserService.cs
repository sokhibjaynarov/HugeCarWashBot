using AutoMapper;
using HugeCarWashBot.Data.IRepositories;
using HugeCarWashBot.Domain.Commons;
using HugeCarWashBot.Domain.Configurations;
using HugeCarWashBot.Domain.Entities.Users;
using HugeCarWashBot.Domain.Enums;
using HugeCarWashBot.Service.DTOs.Users;
using HugeCarWashBot.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace HugeCarWashBot.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
        }
        public async Task<BaseResponse<User>> CreateAsync(UserForCreationDto userDto)
        {
            var response = new BaseResponse<User>();

            // check for user
            var existUser = await unitOfWork.Users.GetAsync(p => p.PhoneNumber == userDto.PhoneNumber);
            if (existUser is not null)
            {
                response.Error = new ErrorResponse(400, "User is exist");
                return response;
            }

            var mappedUser = mapper.Map<User>(userDto);

            mappedUser.Create();

            var result = await unitOfWork.Users.CreateAsync(mappedUser);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for user
            var existUser = await unitOfWork.Users.GetAsync(expression);
            if (existUser is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            existUser.Delete();

            var result = await unitOfWork.Users.UpdateAsync(existUser);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<User>>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<User>>();

            var users = await unitOfWork.Users.GetAllAsync(expression);

            response.Data = users;

            return response;
        }

        public async Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<User>();

            var user = await unitOfWork.Users.GetAsync(expression);
            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            response.Data = user;

            return response;
        }

        public async Task<BaseResponse<User>> UpdateAsync(Guid id, UserForCreationDto userDto)
        {
            var response = new BaseResponse<User>();

            // check for user
            var user = await unitOfWork.Users.GetAsync(p => p.Id == id && p.State != ItemState.Deleted);
            if (user is null)
            {
                response.Error = new ErrorResponse(400, "User is not exist");
                return response;
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.CarNumber = userDto.CarNumber;
            user.CarModel = userDto.CarModel;
            user.PhoneNumber = userDto.PhoneNumber;
            user.TelegramId = userDto.TelegramId;
            user.Step = userDto.Step;

            user.Update();

            var result = await unitOfWork.Users.UpdateAsync(user);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
    }
}
