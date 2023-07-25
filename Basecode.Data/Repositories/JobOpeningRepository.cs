using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class JobOpeningRepository : BaseRepository, IJobOpeningRepository
    {
        /// <summary>
        /// Creates an instance of Basecode context
        /// </summary>
        private readonly BasecodeContext _context;
        public JobOpeningRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieve all Job Openings from the database
        /// </summary>
        /// <returns>An IQueryable of Job Openings.</returns>
        public IQueryable<JobOpening> RetrieveAll()
        {
            return this.GetDbSet<JobOpening>();
        }

        /// <summary>
        /// Retrieves a Job Opening by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Job Opening</returns>
        public JobOpening GetById(int id) 
        {
            return _context.JobOpening.Find(id);
        }

        /// <summary>
        /// Create/Add a new Job Opening
        /// </summary>
        /// <param name="jobOpening"></param>
        public void Add(JobOpening jobOpening)
        {
            _context.JobOpening.Add(jobOpening);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates a selected Job Opening
        /// </summary>
        /// <param name="jobOpening"></param>
        public void Update(JobOpening jobOpening)
        {
            _context.JobOpening.Update(jobOpening);
            _context.SaveChanges();
        }

		/// <summary>
		/// Deletes a selected Job Opening
		/// </summary>
		/// <param name="jobOpening"></param>
		public void Delete(JobOpening job)
        {
            if(job!= null) 
            { 
                _context.JobOpening.Remove(job);
                _context.SaveChanges();
            }
        }

        public JobOpeningViewModel GetMostRecentJobOpening()
        {
            // Fetch the most recent job opening from the database
            var recentJobOpening = _context.JobOpening
                .OrderByDescending(j => j.CreatedTime)
                .FirstOrDefault();

            if (recentJobOpening == null)
            {
                // If no job openings are available, return a specific ViewModel with a message
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
    }
}
