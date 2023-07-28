using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using NLog;
using System;

namespace Basecode.Services.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly ISignUpRepository _repository;
        private readonly IMapper _mapper;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public SignUpService(ISignUpRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates an account in the user management system.
        /// </summary>
        /// <param name="newAccount">An instance of the SignUpViewModel class containing the necessary data for account creation.</param>
        public void CreateAccount(SignUpViewModel newAccount)
        {
            try
            {
                newAccount.CreatedTime = DateTime.Now;
                newAccount.CreatedBy = System.Environment.UserName;

                _repository.CreateAccount(_mapper.Map<SignUp>(newAccount));

                // Log successful creation of the account
                _logger.Info("Account created successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the creation process
                _logger.Error(ex, "Error occurred while creating an account.");
                throw;
            }
        }
    }
}
