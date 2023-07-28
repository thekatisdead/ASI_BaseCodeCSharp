using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        private readonly BasecodeContext _context;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public LoginRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public SignUp GetByUsername(string username)
        {

            try
            {
                // Retrieve the SignUp entity from the database based on the username (case-insensitive)
                SignUp user = _context.UserManagement
                    .Where(x => x.Username.ToLower().Equals(username.ToLower()))
                    .AsNoTracking()
                    .FirstOrDefault();

                _logger.Info($"SignUp entity retrieved by username: {username}");

                if (user == null)
                {
                    // Log a message if the SignUp entity is not found
                    _logger.Info($"SignUp entity not found for username: {username}");
                }

                return user;
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs
                _logger.Error(ex, $"Error occurred while retrieving SignUp entity by username: {username}");
                throw;
            }
        }

    }
}
