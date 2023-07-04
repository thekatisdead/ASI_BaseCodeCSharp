using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class SignUpRepository: BaseRepository, ISignUpRepository
    {
        public readonly BasecodeContext _context;

        public SignUpRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        /// <summary>
        /// Creates an account in the user management system.
        /// </summary>
        /// <param name="newAccount">An instance of the SignUpViewModelclass containing the necessary data for account creation.</param>
        public void CreateAccount(SignUp newAccount)
        {
            _context.UserManagement.Add(newAccount);
            _context.SaveChanges();
        }
    }
}
