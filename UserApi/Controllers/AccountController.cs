using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Authentication;
using UserApi.Data.Services;
using UserApi.Dtos.Entities;
using UserApi.Models;

namespace UserApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AuthenticationManager _authenticationManager;
        public AccountController(IUserService userService, AuthenticationManager authenticationManager)
        {
            _userService = userService;
            _authenticationManager = authenticationManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result =  await _authenticationManager.AuthenticateUser(model.UserName, model.Password);
            if (result.IsAuthenticated)
            {
                var usreDetails = await _userService.GetUserByUserName(model.UserName);
                var token = JwtTokenGenerator.GenerateJwtToken(usreDetails.User);
                var response =  new LoginResponse { Token = token };
                return Ok(response);
            }
            return BadRequest("Invalid username or password.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> AddUser([FromBody] AddUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.Add(new UserDto
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                Password = model.Password,
                UserName = model.UserName
            });

            if (result.Status == Core.Enums.OperationResult.USER_ALREADY_EXISTS)
                return BadRequest("User name is taken");

            return Created("api/user" +
                "/{userId}", new { id = result.UserId });

        }

    }
}
