using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Interfaces
{
    public interface IUserViewRepository
    {
        public IQueryable<SignUp> RetrieveAll();
        SignUp GetUserById(int id);
        void UpdateUser(SignUp user);
        void DeleteUser(SignUp user);
    }
}
