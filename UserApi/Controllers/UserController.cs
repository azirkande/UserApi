using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Data.Services;
using UserApi.Dtos.Entities;
using UserApi.Models;

namespace UserApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

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
                Password =  model.Password,
                UserName = model.UserName
            });

            if (result.Status == Core.Enums.OperationResult.USER_ALREADY_EXISTS)
                return BadRequest("User name is taken");
            return Created("api/user" +
                "/{userId}", new { id = result.UserId });

        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel model, Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.Update(new UserDto
            {
                Id = userId,
                LastName = model.LastName,
                FirstName = model.FirstName,
                UserName = model.UserName
            });
            if (result.Status == Core.Enums.OperationResult.USER_ALREADY_EXISTS)
                return BadRequest();
            return Accepted();
        }

        [HttpPut("users")]
        public async Task<IActionResult> ListUsers()
        {
            var users = await _userService.GetAllUsers();
            return Accepted(users);
        }
    }
}
