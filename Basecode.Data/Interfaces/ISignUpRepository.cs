using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Interfaces
{
    public interface ISignUpRepository
    {
        /// <summary>
        /// Creates a new account in the user management system using the provided user management data.
        /// </summary>
        /// <param name="newAccount">An instance of the SignUpViewModel class containing the necessary data for account creation.</param>
        void CreateAccount(SignUp newAccount);
    }
}
