using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Interfaces
{
    public interface IAdminRepository
    {
        /// <summary>
        /// Creates a new role with the specified roleName if it does not already exist.
        /// </summary>
        /// <param name="roleName">The name of the role to be created.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, which returns an IdentityResult
        /// indicating whether the role creation was successful. If the role already exists, 
        /// null is returned.
        /// </returns>Task<IdentityResult> CreateRole(string roleName);
    }
}
