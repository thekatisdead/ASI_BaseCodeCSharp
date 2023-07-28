using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public LoginService(ILoginRepository loginRepository, IMapper mapper)
        {
            _loginRepository = loginRepository;
            _mapper = mapper;
        }

        public SignUpViewModel GetByUsername(string username)
        {
            try
            {
                var res = _loginRepository.GetByUsername(username);

                // Log successful retrieval of the user by username
                _logger.Info($"Retrieved user by username: {username}");

                return _mapper.Map<SignUpViewModel>(res);
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the retrieval process
                _logger.Error(ex, $"Error occurred while retrieving user by username: {username}");
                throw;
            }
        }
    }
}
