using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.EntityFrameworkCore;
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

        public LoginRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public Login GetByUsername(string username)
        {
            return _context.UserManagement.Where(x => x.Username.ToLower().Equals(username.ToLower())).AsNoTracking().FirstOrDefault();
        }
    }
}
