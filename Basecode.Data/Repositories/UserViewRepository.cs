using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.ViewModels;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;

namespace Basecode.Data.Repositories
{
    public class UserViewRepository: BaseRepository, IUserViewRepository
    {
        private BasecodeContext _context;
        public UserViewRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }
        public IQueryable<SignUp> RetrieveAll()
        {
            return this.GetDbSet<SignUp>();
        }

        public void DeleteUser(SignUp user)
        {
            if (user != null)
            {
                _context.UserManagement.Remove(user);
                _context.SaveChanges();
            }
        }

        public SignUp GetUserById(int id)
        {
            return _context.UserManagement.Find(id);
        }

        public void UpdateUser(SignUp user)
        {
            _context.UserManagement.Update(user);
            _context.SaveChanges();
        }
    }
}
