using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Core.Data.Entities;
using UserApi.Core.Data.Repositories;
using UserApi.Core.Enums;
using UserApi.Dtos.Entities;

namespace UserApi.Data.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();

        Task<UpdateProfileResult> Add(UserDto user);

        Task<UpdateProfileResult> Update(UserDto user);

        Task<UserLoginResult> GetUserByUserName(string userName);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UpdateProfileResult> Add(UserDto user)
        {
            var userToSave = MapDtoToEntity(user);

            var isUserExists = (await _userRepository.GetUserByName(userToSave.UserName)) != null;
            if (isUserExists)
            {
                return new UpdateProfileResult { Status = Core.Enums.OperationResult.USER_ALREADY_EXISTS };
            }
            userToSave.ModifiedOn = DateTime.UtcNow;
            userToSave.CreatedOn = DateTime.UtcNow;
            var id = await _userRepository.Add(userToSave);

            return new UpdateProfileResult { Status = Core.Enums.OperationResult.SUCCESS, UserId = id };
        }

        public async Task<UpdateProfileResult> Update(UserDto user)
        {

            var isExists = await _userRepository.IsDifferentUserWithSameUserNameExists(user.UserName, user.Id);

            if (isExists)
            {
                return new UpdateProfileResult { Status = Core.Enums.OperationResult.USER_ALREADY_EXISTS };
            }
            var userToSave = MapDtoToEntity(user);


            userToSave.ModifiedOn = DateTime.UtcNow;
            await _userRepository.Update(userToSave);

            return new UpdateProfileResult { Status = Core.Enums.OperationResult.SUCCESS };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetUsers();

            var allUsers = users?.Select(user =>
           {
               return MapEntityToDto(user);
           });

            return allUsers;

        }

        private UserDto MapEntityToDto(User entity)
        {
            return new UserDto
            {
                CreatedOn = entity.CreatedOn,
                FirstName = entity.FirstName,
                Id = entity.Id,
                LastName = entity.LastName,
                ModifiedOn = entity.ModifiedOn,
                UserName = entity.UserName
            };
        }

        private User MapDtoToEntity(UserDto dto)
        {
            return new User
            {
                FirstName = dto.FirstName,
                Id = dto.Id,
                LastName = dto.LastName,
                UserName = dto.UserName,
                Password = dto.Password
            };
        }

        public async Task<UserLoginResult> GetUserByUserName(string userName)
        {
            var user = await _userRepository.GetUserByName(userName);
            if (user == null)
                return new UserLoginResult { Status = OperationResult.USER_NOT_FOUND };
            return new UserLoginResult
            {
                Status = OperationResult.SUCCESS,
                User = new UserLoginDetails
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Password = user.Password,
                    UserName = user.UserName
                }
            };
        }
    }
}
