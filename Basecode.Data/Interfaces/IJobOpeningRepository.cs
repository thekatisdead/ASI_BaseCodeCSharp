using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Interfaces
{
    public interface IJobOpeningRepository
    {
        /// <summary>
        /// Retrieve all Job Openings from the database
        /// </summary>
        /// <returns>An IQueryable of Job Openings.</returns>
        IQueryable<JobOpening> RetrieveAll();

        /// <summary>
        /// Retrieves a Job Opening by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Job Opening</returns>
        JobOpening GetById(int id);

        /// <summary>
        /// Create/Add a new Job Opening
        /// </summary>
        /// <param name="jobOpening"></param>
        void Add(JobOpening jobOpening);

        /// <summary>
        /// Updates a selected Job Opening
        /// </summary>
        /// <param name="jobOpening"></param>
        void Update(JobOpening jobOpening);
        public void Delete(JobOpening job);

        /// <summary>
        /// Retrieves the most recent job opening from the database.
        /// </summary>
        /// <returns>The most recent job opening.</returns>
        JobOpeningViewModel GetMostRecentJobOpening();
    }
}
