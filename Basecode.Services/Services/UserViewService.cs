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
    }
}
