﻿using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Basecode.Services.Interfaces
{
    public interface IUserService
    {
        User FindByUsername(string username);
        User FindByEmail(string email);
        User FindById(string id);
        IdentityUser FindUser(string userName);
        IEnumerable<User> FindAll();
        bool Create(User user);
        bool Update(User user);
        void Delete(User user);
        Task<IdentityResult> RegisterUser(string username, string password, string firstName, string lastName, string email, string role);
        Task<IdentityResult> CreateRole(string roleName);
        Task<IdentityUser> FindUser(string username, string password);
        Task<IdentityUser> FindUserAsync(string userName, string password);
        public List<UserViewModel> RetrieveAll();
    }
}
