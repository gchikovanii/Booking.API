using Booking.API.Constants;
using Booking.Application.Models.IdentityUsers;
using Booking.Application.Models.Roles;
using Booking.Application.Services.Abstraction.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(nameof(CreateAdmin))]
        [Authorize(Roles = UserType.Admin)]

        public async Task<IActionResult> CreateAdmin(CreateUserDto input)
        {
            await _userService.CreateAdminUser(input);
            return Ok();

        }
        [HttpPost(nameof(CreateUser))]
        [Authorize(Roles = UserType.AdminSuperVisor)]

        public async Task<IActionResult> CreateUser(CreateUserDto input)
        {
            await _userService.CreateUser(input);
            return Ok();

        }

        [HttpPost(nameof(CreateSuperVisor))]
        [Authorize(Roles = UserType.Admin)]

        public async Task<IActionResult> CreateSuperVisor(CreateUserDto input)
        {
            await _userService.CreateSupervisor(input);
            return Ok();
        }

        [HttpPost(nameof(CreateRole))]
        [Authorize(Roles = UserType.AdminSuperVisor)]

        public async Task<IActionResult> CreateRole(RoleDto input)
        {
            await _userService.CreateUserRole(input);
            return Ok();
        }

        [HttpDelete(nameof(DeleteUser))]
        [Authorize(Roles = UserType.AdminSuperVisor)]
        public async Task<IActionResult> DeleteUser(DeleteUserDto input)
        {
            await _userService.DeleteUser(input);
            return Ok();
        }

        [HttpPut(nameof(ChangeUserName))]
        [Authorize(Roles = UserType.AdminUser)]

        public async Task<IActionResult> ChangeUserName([FromForm] UpdateUserNameDto input)
        {
            await _userService.ChangeUserName(input);
            return Ok();
        }

        #region Change Password
        //[HttpPut(nameof(ChangeUserPassword))]
        //public async Task<IActionResult> ChangeUserPassword([FromForm] UpdateUserPasswordDto input)
        //{
        //    await _userService.ChangeUserPassword(input);
        //    return Ok();
        //}
        #endregion




    }
}
