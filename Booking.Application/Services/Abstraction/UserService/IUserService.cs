using Booking.Application.Models.IdentityUsers;
using Booking.Application.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services.Abstraction.UserService
{
    public interface IUserService
    {
        Task CreateAdminUser(CreateUserDto input);
        Task CreateUserRole(RoleDto input);
        Task CreateUser(CreateUserDto input);
        Task DeleteUser(DeleteUserDto input);
        Task CreateSupervisor(CreateUserDto input);

        Task<bool> ChangeUserName(UpdateUserNameDto input);

        #region Change Password
        //Task ChangeUserPassword(UpdateUserPasswordDto input);
        #endregion
    }
}
