﻿using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class ApplicantListService: IApplicantListService
    {
        private readonly IApplicantListRepository _repository;
        private readonly DbContext _dbContext;

        public ApplicantListService(IApplicantListRepository repository) 
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all applicants from the repository.
        /// </summary>
        /// <returns>A list of ApplicantListViewModel objects representing all applicants.</returns>
        public List<ApplicantListViewModel> RetrieveAll()
        {
            var data = _repository.RetrieveAll().Select(s => new ApplicantListViewModel
            {
                Id = s.Id,
                Firstname = s.Firstname,
                Lastname = s.Lastname,
                JobApplied = s.JobApplied
            }).ToList();

            return data;
        }

        public void UpdateApplicant(ApplicantListViewModel applicant)
        {
            _dbContext.Update(applicant);
            _dbContext.SaveChanges();
        }
    }
}
