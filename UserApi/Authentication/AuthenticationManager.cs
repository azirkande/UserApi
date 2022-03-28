using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Core.Enums;
using UserApi.Data.Services;

namespace UserApi.Authentication
{
    public record AuthResult
    {
        public bool IsAuthenticated { get; init; }
        public string Name { get; init; }
        public Guid Id { get; init; }
    }
    public interface IAuthenticationManager
    {
        Task<AuthResult> AuthenticateUser(string userName, string secret);
    }

    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IUserService _userService;

        public AuthenticationManager(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AuthResult> AuthenticateUser(string userName, string secret)
        {
            var result = await _userService.GetUserByUserName(userName);
            if (result.Status != OperationResult.SUCCESS)
                return new AuthResult { IsAuthenticated = false };

            if (result.User.Password.ToLower().Equals(secret))
                return new AuthResult { IsAuthenticated = true, Id = result.User.Id, Name = string.Join(result.User.FirstName, "  ", result.User.LastName) };

            return new AuthResult { IsAuthenticated = false };
        }

    }
}
