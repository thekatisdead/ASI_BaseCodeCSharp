﻿using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Basecode.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Creates an instance of Basecode context
        /// </summary>

        public UserRepository(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
            : base(unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IEnumerable<User> FindAll()
        {
            return base.GetDbSet<User>();
        }

        public User FindByUsername(string username)
        {
            return GetDbSet<User>().Where(x => x.Username.ToLower().Equals(username.ToLower())).AsNoTracking().FirstOrDefault();
        }

        public User FindByEmail(string email)
        {
            return GetDbSet<User>().Where(x => x.Email.ToLower().Equals(email.ToLower())).AsNoTracking().FirstOrDefault();
        }

        public User FindById(string id)
        {
            return GetDbSet<User>().FirstOrDefault(x => x.Id.Equals(id));
        }

        public IdentityUser FindUser(string userName)
        {
            var userDB = GetDbSet<IdentityUser>().Where(x => x.UserName.ToLower().Equals(userName.ToLower())).AsNoTracking().FirstOrDefault();
            return userDB;
        }

        public bool Create(User user)
        {
            try
            {
                base.GetDbSet<User>().Add(user);
                UnitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

        public bool Update(User user)
        {
            try
            {
                SetEntityState(user, EntityState.Modified);
                UnitOfWork.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void Delete(User user)
        {
            base.GetDbSet<User>().Remove(user);
            UnitOfWork.SaveChanges();
        }

        public async Task<IdentityResult> RegisterUser(string username, string password, string firstName, string lastName, string email, string role)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded) return result;

            bool checkIfRoleExists = await _roleManager.RoleExistsAsync(role);
            if (checkIfRoleExists)
            {
                var result1 = await _userManager.AddToRoleAsync(user, role);

                if (!result1.Succeeded) return result1;
            }
         
            var userId = user.Id;

            // Insert user details
            var userEntity = new User
            {
                Id = userId,
                Username = username,
                FirstName = firstName,
                LastName = lastName,
            };

            Create(userEntity);

            return result;
        }
        public async Task<IdentityResult> CreateRole(string roleName)
        {
            bool checkIfRoleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!checkIfRoleExists)
            {
                var role = new IdentityRole();
                role.Name = roleName;
                var result = await _roleManager.CreateAsync(role);
                return result;
            }

            return null;
        }        

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<IdentityUser> FindUserAsync(string userName, string password)
        {
            var userDB = GetDbSet<IdentityUser>().Where(x => x.UserName.ToLower().Equals(userName.ToLower())).AsNoTracking().FirstOrDefault();
            var user = await _userManager.FindByNameAsync(userName);
            var isPasswordOK = await _userManager.CheckPasswordAsync(user, password);
            if ((user == null) || (isPasswordOK == false))
            {
                userDB = null;
            }
            return userDB;
        }

        public IQueryable<User> RetrieveAll()
        {
            return this.GetDbSet<User>();
        }

        public void UpdateUser(User user)
        {
            GetDbSet<User>().Update(user);
            UnitOfWork.SaveChanges();
        }
    }
}
