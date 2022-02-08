using BarisTutakli.Week4.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Business
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public  async Task<IdentityResult> CreateUser(RegisterUserModel model)
        {
            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task<RegisterAdminResponse> CreateAdmin(RegisterUserModel model)
        {
            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            return new RegisterAdminResponse{ Result = result, User = user};
        }

        public async Task<User> FindByNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user;
        }

        public async Task<bool> CheckUser(User user, UserLoginModel model)
        {
            return user != null && await _userManager.CheckPasswordAsync(user, model.Password);
        }

        public async Task<bool> CreateAdminRole(User user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));
            if (!await _roleManager.RoleExistsAsync(Roles.User))
                await _roleManager.CreateAsync(new IdentityRole(Roles.User));

            if (await _roleManager.RoleExistsAsync(role))
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            return true;
        }
    }
}
