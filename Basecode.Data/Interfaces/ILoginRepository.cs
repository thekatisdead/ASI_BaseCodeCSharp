using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Interfaces
{
    public interface ILoginRepository
    {
        /// <summary>
        /// Retrieves a SignUp object by the given username.
        /// </summary>
        /// <param name="username">The username associated with the SignUp object to retrieve.</param>
        /// <returns>The SignUp object associated with the provided username.</returns>
        SignUp GetByUsername(string username);
    }
}
