using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using NLog;
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
        private static Logger _logger = LogManager.GetCurrentClassLogger();

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
            try
            {
                _context.UserManagement.Add(newAccount);
                _context.SaveChanges();

                // Log successful account creation
                _logger.Info($"Account created successfully for username: {newAccount.Username}");
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the account creation process
                _logger.Error(ex, $"Error occurred while creating account for username: {newAccount.Username}");
                throw;
            }
        }
    }
}
