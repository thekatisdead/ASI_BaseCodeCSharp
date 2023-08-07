using Basecode.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Basecode.Data.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The User object if found, otherwise null.</returns>
        User FindByUsername(string username);

        User FindByEmail(string email);

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The User object if found, otherwise null.</returns>
        User FindById(string id);

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="UserName">The username of the user to retrieve.</param>
        /// <returns>The User object if found, otherwise null.</returns>
        IdentityUser FindUser(string UserName);

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>An IEnumerable of User objects.</returns>
        IEnumerable<User> FindAll();

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The User object to create.</param>
        /// <returns>True if the user is created successfully, otherwise false.</returns>
        bool Create(User user);

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="user">The User object to update.</param>
        /// <returns>True if the user is updated successfully, otherwise false.</returns>
        void Update(User user);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="user">The User object to delete.</param>
        void Delete(User user);

        /// <summary>
        /// Registers a new user with the specified details and assigns a role.
        /// </summary>
        /// <param name="username">The username of the new user.</param>
        /// <param name="password">The password of the new user.</param>
        /// <param name="firstName">The first name of the new user.</param>
        /// <param name="lastName">The last name of the new user.</param>
        /// <param name="email">The email address of the new user.</param>
        /// <param name="role">The role to assign to the new user.</param>
        /// <returns>An IdentityResult representing the result of the user registration.</returns>
        Task<IdentityResult> RegisterUser(string username, string password, string firstName, string lastName, string email, string role);

        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        /// <returns>An IdentityResult representing the result of the role creation.</returns>
        Task<IdentityResult> CreateRole(string roleName);

        /// <summary>
        /// Finds a user by their username and password.
        /// </summary>
        /// <param name="userName">The username of the user to find.</param>
        /// <param name="password">The password of the user to find.</param>
        /// <returns>An IdentityUser object if found, otherwise null.</returns>
        Task<IdentityUser> FindUser(string userName, string password);

        /// <summary>
        /// Asynchronously finds a user by their username and password.
        /// </summary>
        /// <param name="userName">The username of the user to find.</param>
        /// <param name="password">The password of the user to find.</param>
        /// <returns>A Task representing the asynchronous operation that returns a User object if found, otherwise null.</returns>
        Task<IdentityUser> FindUserAsync(string userName, string password);
        public IQueryable<User> RetrieveAll();
    }
}
