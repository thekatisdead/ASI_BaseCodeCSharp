using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NLog;
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
        private readonly IJobOpeningRepository _jobOpeningRepository;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public ApplicantListService(IApplicantListRepository repository,IJobOpeningRepository jobOpeningRepository)
        {
            _repository = repository;
            _jobOpeningRepository = jobOpeningRepository;
        }

        /// <summary>
        /// Retrieves all applicants from the repository.
        /// </summary>
        /// <returns>A list of ApplicantListViewModel objects representing all applicants.</returns>
        public List<ApplicantListViewModel> RetrieveAll()
        {
            try
            {
                var jobs = _jobOpeningRepository.RetrieveAll().Select(j => new
                {
                    Id = j.Id,
                    Position = j.Position
                });
                var applicants = _repository.RetrieveAll().Select(s => new
                {
                    Id = s.Id,
                    FormID = s.FormID,
                    Firstname = s.Firstname,
                    Lastname = s.Lastname,
                    JobApplied = s.JobApplied,
                    Tracker = s.Tracker,
                    Grading = s.Grading
                });

                var data = from app in applicants
                           join job in jobs on app.JobApplied equals job.Id
                           select new ApplicantListViewModel
                           {
                               Id = app.Id,
                               FormID = app.FormID,
                               Firstname = app.Firstname,
                               Lastname = app.Lastname,
                               JobApplied = app.JobApplied,
                               JobPosition = job.Position,
                               Tracker = app.Tracker,
                               Grading = app.Grading
                           };
                // Log successful retrieval of the applicant list
                _logger.Info("Successfully retrieved the list of applicants.");
                return data.ToList();
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the retrieval process
                _logger.Error(ex, "Error occurred while retrieving the list of applicants.");
                throw;
            }

        }

        // old function, stored just in case 
        /*public void UpdateStatus(int applicantId, string status)
        {
            _repository.UpdateStatus(applicantId, status);
        }*/

        public void ProceedTo(int applicantId, string step)
        {
            try
            {
                _repository.ProceedTo(applicantId, step);

                // Log successful proceeding to the next step
                _logger.Info($"Applicant {applicantId} proceeded to step: {step}");
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the proceeding process
                _logger.Error(ex, $"Error occurred while proceeding applicant {applicantId} to step: {step}");
                throw;
            }
        }

        public void UpdateStatus(int applicantID, string status)
        {
            try
            {
                var _applicant = _repository.GetById(applicantID);
                _applicant.Tracker = status;

                _repository.Update(_applicant);

                // Log successful status update
                _logger.Info($"Status updated successfully for applicantID: {applicantID}, new status: {status}");
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the status update process
                _logger.Error(ex, $"Error occurred while updating status for applicantID: {applicantID}, new status: {status}");
                throw;
            }
        }

        public void UpdateGrade(int applicantID, string grade)
        {
            try
            {
                var _applicant = _repository.GetById(applicantID);
                _applicant.Grading = grade;

                _repository.Update(_applicant);

                // Log successful grade update
                _logger.Info($"Grade updated successfully for applicantID: {applicantID}, new grade: {grade}");
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the grade update process
                _logger.Error(ex, $"Error occurred while updating grade for applicantID: {applicantID}, new grade: {grade}");
                throw;
            }
        }

        public ApplicantListViewModel GetMostRecentApplicant()
        {
            try
            {
                // Fetch the most recent applicant's info from the repository
                var recentApplicant = _repository.GetMostRecentApplicant();

                // Check if a recent applicant exists
                if (recentApplicant == null)
                {
                    // Log that no recent applicant was found
                    _logger.Info("No recent applicant found.");
                    return null;
                }

                // Map the recent applicant to ApplicantListViewModel
                var applicantViewModel = new ApplicantListViewModel
                {
                    Id = recentApplicant.Id,
                    Firstname = recentApplicant.Firstname,
                    Lastname = recentApplicant.Lastname,
                    JobApplied = recentApplicant.JobApplied,
                    Tracker = recentApplicant.Tracker,
                    Grading = recentApplicant.Grading
                };

                // Log successful retrieval of the most recent applicant
                _logger.Info($"Most recent applicant retrieved with ID: {recentApplicant.Id}");

                return applicantViewModel;
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the retrieval process
                _logger.Error(ex, "Error occurred while retrieving the most recent applicant.");
                throw;
            }
        }

        public ApplicantListViewModel GetMostRecentApplicantForRequirements()
        {
            try
            {
                // Fetch the most recent applicant's info from the repository
                var recentApplicant = _repository.GetMostRecentApplicant();

                // Check if a recent applicant exists
                if (recentApplicant == null)
                {
                    // Log that no recent applicant was found
                    _logger.Info("No recent applicant found for requirements.");
                    return null;
                }

                // Map the recent applicant to ApplicantListViewModel
                var applicantViewModel = new ApplicantListViewModel
                {
                    Id = recentApplicant.Id,
                    Firstname = recentApplicant.Firstname,
                    Lastname = recentApplicant.Lastname,
                    JobApplied = recentApplicant.JobApplied,
                    Grading = recentApplicant.Grading
                };

                // Log successful retrieval of the most recent applicant for requirements
                _logger.Info($"Most recent applicant retrieved with ID: {recentApplicant.Id} for requirements.");

                return applicantViewModel;
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the retrieval process
                _logger.Error(ex, "Error occurred while retrieving the most recent applicant for requirements.");
                throw;
            }
        }
        public Applicant GetApplicantById(int id)
        {
            return _repository.GetById(id);
        }
    }
}
