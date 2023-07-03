using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface ISignUpService
    {
        /// <summary>
        /// Creates an account in the user management system.
        /// </summary>
        /// <param name="newAccount">An instance of the SignUpViewModel class containing the necessary data for account creation.</param>
        void CreateAccount(SignUpViewModel newAccount);
    }
}
