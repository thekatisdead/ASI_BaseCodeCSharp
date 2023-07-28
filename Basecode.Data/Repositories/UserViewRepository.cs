﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.ViewModels;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using NLog;

namespace Basecode.Data.Repositories
{
    public class UserViewRepository : BaseRepository, IUserViewRepository
    {
        private BasecodeContext _context;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UserViewRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public IQueryable<SignUp> RetrieveAll()
        {
            try
            {
                _logger.Info("Retrieving all users from the database.");
                return this.GetDbSet<SignUp>();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while retrieving all user views: {errorMessage}", ex.Message);
                throw;
            }
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

        public void AddUser(SignUp user)
        {
            _context.UserManagement.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(SignUp user)
        {
            _context.UserManagement.Update(user);
            _context.SaveChanges();
        }
    }
}