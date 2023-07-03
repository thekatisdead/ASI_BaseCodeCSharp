using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using System;

namespace Basecode.Services.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly ISignUpRepository _repository;

        public SignUpService(ISignUpRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates an account in the user management system.
        /// </summary>
        /// <param name="newAccount">An instance of the SignUpViewModel class containing the necessary data for account creation.</param>
        public void CreateAccount(SignUpViewModel newAccount)
        {
            newAccount.CreatedTime = DateTime.Now;
            newAccount.CreatedBy = System.Environment.UserName;

            _repository.CreateAccount(newAccount);
        }
    }
}
