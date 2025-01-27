﻿using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services .Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Basecode.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public User FindByUsername(string username)
        {
            return _userRepository.FindByUsername(username);
        }

        public User FindById(string id)
        {
            return _userRepository.FindById(id);
        }
         
        public IdentityUser FindUser(string userName)
        {
            return _userRepository.FindUser(userName);
        }

        public IEnumerable<User> FindAll()
        {
            return _userRepository.FindAll();
        }

        public bool Create(User user)
        {
            user.CreatedBy = System.Environment.UserName;
            user.CreatedDate = DateTime.Now;
            user.ModifiedBy = System.Environment.UserName;
            user.ModifiedDate = DateTime.Now;
            return _userRepository.Create(user);
        }

        public void Update(User user)
        {
            var s = _userRepository.FindByUsername(user.Username);
            if (s != null)
            {
                // Update the properties of the retrieved user object with the new values
                s.FirstName = user.FirstName;
                s.LastName = user.LastName;
                s.ContactNumber = user.ContactNumber;
                s.Email = user.Email;
                s.Address = user.Address;
                s.ModifiedBy = System.Environment.UserName;
                s.ModifiedDate = DateTime.Now;

                // Save the changes to the database
                _userRepository.Update(s);
            }
        }

        public void Delete(User user)
        {
            _userRepository.Delete(user);
        }

        public Task<IdentityUser> FindUserAsync(string userName, string password)
        {
            return _userRepository.FindUserAsync(userName, password);
        }

        public async Task<IdentityResult> RegisterUser(string username, string password, string firstName, string lastName, string email, string role)
        {
            return await _userRepository.RegisterUser(username, password, firstName, lastName, email, role);
        }
        public async Task<IdentityResult> CreateRole(string roleName)
        {
            return await _userRepository.CreateRole(roleName);
        }

        public async Task<IdentityUser> FindUser(string username, string password)
        {
            return await _userRepository.FindUser(username, password);
        }

        public List<UserViewModel> RetrieveAll()
        {
            var data = _userRepository.RetrieveAll().Select(s => new UserViewModel
            {
                Username = s.Username,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                ContactNumber = s.ContactNumber,
                Address = s.Address 
            });

            return data.ToList();
        }
    }
}
