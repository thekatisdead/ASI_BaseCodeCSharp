using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface ILoginService
    {
        /// <summary>
        /// Lets user login to the system.
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <returns>Returns a SignUpViewModel</returns>
        SignUpViewModel GetByUsername(string username);
    }
}
