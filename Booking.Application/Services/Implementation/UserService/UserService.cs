using Booking.Application.Constants;
using Booking.Application.Models.IdentityUsers;
using Booking.Application.Models.Roles;
using Booking.Application.Services.Abstraction.UserService;
using Booking.Domain.Entities.UserAggregate;
using Booking.Infrastructure.Repository.Abstraction;
using Booking.Infrastructure.Repository.Implementation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
        }
        //Create User Roles!
        public async Task CreateUserRole(RoleDto input)
        {
            var existRole = await _roleManager.FindByNameAsync(input.RoleType.ToString());
            if(existRole == null)
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = input.RoleType.ToString()
                });
            }
        }

        //Create User which will be Admin!
        public async Task CreateAdminUser(CreateUserDto input)
        {
            var existingUser = await _userManager.FindByEmailAsync(input.Email);

            if(existingUser == null)
            {
                var currentUser = new AppUser()
                {
                    UserName = input.UserName,
                    Email = input.Email,
                };
                var createdUser = await _userManager.CreateAsync(currentUser, input.Password);

                if (createdUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(currentUser,RoleType.Admin.ToString());
                }
            }
        }

        //Create User
        public async Task CreateUser(CreateUserDto input)
        {
            var existingUser = await _userManager.FindByEmailAsync(input.Email);
            if(existingUser == null)
            {
                var currentUser = new AppUser()
                {
                    UserName = input.UserName,
                    Email = input.Email,
                    
                };

                var createdUser = await _userManager.CreateAsync(currentUser, input.Password);
            
                if(createdUser.Succeeded)
                    await _userManager.AddToRoleAsync(currentUser,RoleType.User.ToString());
            }
        }
        //Create Supervisor
        public async Task CreateSupervisor(CreateUserDto input)
        {
            var existingUser = await _userManager.FindByEmailAsync(input.Email);
            if (existingUser == null)
            {
                var currentUser = new AppUser()
                {
                    UserName = input.UserName,
                    Email = input.Email,

                };

                var createdUser = await _userManager.CreateAsync(currentUser, input.Password);

                if (createdUser.Succeeded)
                    await _userManager.AddToRoleAsync(currentUser, RoleType.SuperVisor.ToString());
            }
        }


        //Delete User
        public async Task DeleteUser(DeleteUserDto input)
        {
            var userExists = await _userManager.FindByEmailAsync(input.Email);
            if(userExists != null)
            {
                await _userManager.DeleteAsync(userExists);
            }
        }

        //Update User Name
        public async Task<bool> ChangeUserName(UpdateUserNameDto input)
        {
            var userExists = await _userManager.FindByEmailAsync(input.Email);
            if (userExists != null)
            {
                if (input.UserName != null)
                    userExists.UserName = input.UserName;
                _userRepository.Update(userExists);
                return await _userRepository.SaveChangesAsync();
            }
            else
                return false;
        }


        #region Update User Password

        ////Update User Password

        //public async Task ChangeUserPassword(UpdateUserPasswordDto input)
        //{
        //    var user = await _userManager.FindByEmailAsync(input.Email);
        //    string? password = input.Password;
        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    await _userManager.ResetPasswordAsync(user, token, password);

        //}
        #endregion
    }
}
