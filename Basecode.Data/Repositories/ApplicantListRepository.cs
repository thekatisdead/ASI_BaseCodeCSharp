using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Linq;

namespace Basecode.Data.Repositories
{
    public class ApplicantListRepository : BaseRepository, IApplicantListRepository
    {
        private readonly BasecodeContext _context;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ApplicantListRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public IQueryable<Applicant> RetrieveAll()
        {
            _logger.Info("Retrieving all applicants from the database.");
            return this.GetDbSet<Applicant>();
        }

        public void Add(Applicant applicant)
        {
            _context.Applicant.Add(applicant);
            _context.SaveChanges();
        }

        public Applicant GetById(int id)
        {
            _logger.Info("Retrieving applicant by ID: {applicantId}", id);
            return _context.Applicant.FirstOrDefault(a => a.Id == id);
        }

        public Applicant GetByFormId(int id)
        {
            // warning here is that it is possible that the return below
            // will return a null value
            return _context.Applicant.FirstOrDefault(a => a.FormId == id);
        }
        public void Update(Applicant applicant)
        {
            try
            {
                _context.Update(applicant);
                _context.SaveChanges();
                _logger.Info("Applicant with ID {applicantId} updated successfully.", applicant.Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating applicant with ID {applicantId}: {errorMessage}", applicant.Id, ex.Message);
                throw;
            }
        }

        public void UpdateStatus(int applicantId, string status)
        {
            try
            {
                var applicant = _context.Applicant.Find(applicantId);
                if (applicant != null)
                {
                    applicant.Grading = status;
                    _context.SaveChanges();
                    _logger.Info("Applicant with ID {applicantId} status updated to {status}.", applicantId, status);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating status for applicant with ID {applicantId}: {errorMessage}", applicantId, ex.Message);
                throw;
            }
        }

        public void ProceedTo(int applicantId, string step)
        {
            try
            {
                var applicant = _context.Applicant.Find(applicantId);
                if (applicant != null)
                {
                    applicant.Tracker = step;
                    applicant.Grading = "On Going";
                    _context.SaveChanges();
                    _logger.Info("Applicant with ID {applicantId} proceeded to step {step}.", applicantId, step);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while proceeding applicant with ID {applicantId} to step {step}: {errorMessage}", applicantId, step, ex.Message);
                throw;
            }
        }

        public void UpdateConfirmed(int applicantId, string status)
        {
            try
            {
                var applicant = _context.Applicant.Find(applicantId);
                if (applicant != null)
                {
                    applicant.Confirmed = status;
                    _context.SaveChanges();
                    _logger.Info("Applicant with ID {applicantId} has updated their requirements to {status}.", applicantId, status);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while proceeding applicant with ID {applicantId} to step {step}: {errorMessage}", applicantId, status, ex.Message);
                throw;
            }
        }

        public ApplicantListViewModel GetMostRecentApplicant()
        {
            try
            {
                _logger.Info("Fetching the most recent applicant from the database.");
                var recentApplicant = _context.Applicant
                    .OrderByDescending(j => j.CreatedTime)
                    .FirstOrDefault();

                if (recentApplicant == null)
                {
                    _logger.Warn("No recent applicant found. Returning a default ViewModel.");
                    return new ApplicantListViewModel
                    {
                        Firstname = "N/A",
                        Lastname = "N/A",
                        Tracker = "N/A",
                        JobApplied = 0
                    };
                }

                _logger.Info("Mapping the Applicant model to ApplicantListViewModel for the most recent applicant.");
                var recentApplicantViewModel = new ApplicantListViewModel
                {
                    Id = recentApplicant.Id,
                    Firstname = recentApplicant.Firstname,
                    Lastname = recentApplicant.Lastname,
                    Tracker = recentApplicant.Tracker,
                    Grading = recentApplicant.Grading,
                    JobApplied = recentApplicant.JobApplied
                };

                return recentApplicantViewModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while getting the most recent applicant: {errorMessage}", ex.Message);
                throw;
            }
        }

        public ApplicantListViewModel GetMostRecentApplicantForRequirements()
        {
            try
            {
                _logger.Info("Fetching the most recent applicant from the database for requirements.");
                var recentApplicant = _context.Applicant
                    .OrderByDescending(j => j.CreatedTime)
                    .FirstOrDefault();

                if (recentApplicant == null)
                {
                    _logger.Warn("No recent applicant found for requirements. Returning a default ViewModel.");
                    return new ApplicantListViewModel
                    {
                        Firstname = "N/A",
                        Lastname = "N/A",
                        JobApplied = 0
                    };
                }

                _logger.Info("Mapping the Applicant model to ApplicantListViewModel for the most recent applicant.");
                var recentApplicantViewModel = new ApplicantListViewModel
                {
                    Id = recentApplicant.Id,
                    Firstname = recentApplicant.Firstname,
                    Lastname = recentApplicant.Lastname,
                    Tracker = recentApplicant.Tracker, 
                    Grading = recentApplicant.Grading,
                    JobApplied = recentApplicant.JobApplied
                };

                return recentApplicantViewModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while getting the most recent applicant for requirements: {errorMessage}", ex.Message);
                throw;
            }
        }
    }
}