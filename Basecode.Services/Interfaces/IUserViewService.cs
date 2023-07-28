using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface IUserViewService
    {
        public List<SignUpViewModel> RetrieveAll();
        SignUpViewModel GetUserById(int id);
        void UpdateUser(SignUp user);
        void DeleteUser(int id);
    }
}
