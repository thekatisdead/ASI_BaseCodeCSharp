using AutoMapper;
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
    public class ApplicantListService : IApplicantListService
    {
        private readonly IApplicantListRepository _repository;

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
                JobApplied = s.JobApplied,
                Tracker = s.Tracker
            }).ToList();

            return data;
        }

        // old function, stored just in case 
        /*public void UpdateStatus(int applicantId, string status)
        {
            _repository.UpdateStatus(applicantId, status);
        }*/

        public void ProceedTo(int applicantId, string step)
        {
            _repository.ProceedTo(applicantId, step);
        }
        public void UpdateStatus(int applicantID, string status)
        {
            var _applicant = _repository.GetById(applicantID);
            _applicant.Tracker = status;

            _repository.Update(_applicant);
        }
        public void UpdateGrade(int applicantID, string grade)
        {
            var _applicant = _repository.GetById(applicantID);
            _applicant.Grading = grade;

            _repository.Update(_applicant);
        }

        public ApplicantListViewModel GetMostRecentApplicant()
        {
            // Fetch the most recent applicant's info from the repository and map it to ApplicantListViewModel
            var recentApplicant = _repository.GetMostRecentApplicant();
            var applicantViewModel = new ApplicantListViewModel
            {
                Id = recentApplicant.Id,
                Firstname = recentApplicant.Firstname,
                Lastname = recentApplicant.Lastname,
                JobApplied = recentApplicant.JobApplied,
                Tracker = recentApplicant.Tracker
            };

            return applicantViewModel;
        }
        public Applicant GetApplicantById(int id)
        {
            return _repository.GetById(id);
        }
    }
}
