using BarisTutakli.Week4.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Business
{
    public interface IUserService
    {
        public Task<IdentityResult> CreateUser(RegisterUserModel model);
        public Task<RegisterAdminResponse> CreateAdmin(RegisterUserModel model);
        public Task<User> FindByNameAsync(string username);

        public Task<bool> CheckUser(User user, UserLoginModel model);
        public Task<bool> CreateAdminRole(User user, string role);
    }
}
