using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class JobOpeningRepository : BaseRepository, IJobOpeningRepository
    {
        private readonly BasecodeContext _context;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public JobOpeningRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public IQueryable<JobOpening> RetrieveAll()
        {
            _logger.Info("Retrieving all Job Openings from the database.");
            return this.GetDbSet<JobOpening>();
        }

        public JobOpening GetById(int id)
        {
            _logger.Info("Retrieving Job Opening by ID: {jobOpeningId}", id);
            return _context.JobOpening.Find(id);
        }

        public void Add(JobOpening jobOpening)
        {
            try
            {
                _context.JobOpening.Add(jobOpening);
                _context.SaveChanges();
                _logger.Info("Job Opening added successfully. ID: {jobOpeningId}", jobOpening.Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while adding Job Opening: {errorMessage}", ex.Message);
                throw;
            }
        }

        public void Update(JobOpening jobOpening)
        {
            try
            {
                _context.JobOpening.Update(jobOpening);
                _context.SaveChanges();
                _logger.Info("Job Opening with ID {jobOpeningId} updated successfully.", jobOpening.Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating Job Opening with ID {jobOpeningId}: {errorMessage}", jobOpening.Id, ex.Message);
                throw;
            }
        }

        public void Delete(JobOpening job)
        {
            try
            {
                if (job != null)
                {
                    _context.JobOpening.Remove(job);
                    _context.SaveChanges();
                    _logger.Info("Job Opening with ID {jobOpeningId} deleted successfully.", job.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting Job Opening with ID {jobOpeningId}: {errorMessage}", job?.Id, ex.Message);
                throw;
            }
        }

        public JobOpeningViewModel GetMostRecentJobOpening()
        {
            try
            {
                // Fetch the most recent job opening from the database
                _logger.Info("Fetching the most recent job opening from the database.");
                var recentJobOpening = _context.JobOpening
                    .OrderByDescending(j => j.CreatedTime)
                    .FirstOrDefault();

                if (recentJobOpening == null)
                {
                    // If no job openings are available, return a specific ViewModel with a message
                    _logger.Warn("No recent job opening found. Returning a default ViewModel.");
                    return new JobOpeningViewModel
                    {
                        Position = "No job openings available",
                        JobType = "N/A",
                        Salary = 0,
                        Hours = 0,
                        Shift = "N/A",
                        Description = "N/A"
                    };
                }

                // Map the JobOpening model to JobOpeningViewModel
                _logger.Info("Mapping the Job Opening Model to JobOpeningViewModel for the most recent job opening.");
                var recentJobOpeningViewModel = new JobOpeningViewModel
                {
                    Id = recentJobOpening.Id,
                    Position = recentJobOpening.Position,
                    JobType = recentJobOpening.JobType,
                    Salary = recentJobOpening.Salary,
                    Hours = recentJobOpening.Hours,
                    Shift = recentJobOpening.Shift,
                    Description = recentJobOpening.Description
                };

                return recentJobOpeningViewModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while getting the most recent Job Opening: {errorMessage}", ex.Message);
                throw;
            }
        }
    }
}