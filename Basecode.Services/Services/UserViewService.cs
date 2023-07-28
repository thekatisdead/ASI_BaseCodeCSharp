using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.Data.Models;
using AutoMapper;
using Hangfire.Common;

namespace Basecode.Services.Services
{
    public class UserViewService:IUserViewService
    {
        private readonly IUserViewRepository _repository;
        private readonly IMapper _mapper;

        public UserViewService(IUserViewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void AddUser(SignUp user)
        {
            _repository.AddUser(user);
        }

        public void DeleteUser(int id)
        {
            var user = _repository.GetUserById(id);
            _repository.DeleteUser(user);
        }

        public UserViewModel GetUserById(int id)
        {
            var data = _repository.GetUserById(id);
            return _mapper.Map<UserViewModel>(data);
        }

        public List<UserViewModel> RetrieveAll() 
        {
            var data = _repository.RetrieveAll().Select( s => new UserViewModel
            {
                UserName =s.Username,
                Password =s.Password,
                ConfirmPassword = s.ConfirmPassword,
                FirstName = s.FirstName,
                LastName = s.LastName,
                EmailAddress = s.EmailAddress,
                Address = s.Address,
                RoleName = s.Role
            }) ;

            return data.ToList();
        }

        public void UpdateUser(SignUp user)
        {
            var s = _repository.GetUserById(user.Id);
            s.Username = user.Username;
            s.Password = user.Password;
            s.ConfirmPassword = user.ConfirmPassword;
            s.FirstName = user.FirstName;
            s.LastName = user.LastName;
            s.EmailAddress = user.EmailAddress;
            s.ContactNumber = user.ContactNumber;
            s.Address = user.Address;
            s.Role = user.Role;

            _repository.UpdateUser(s);
        }
    }
}
