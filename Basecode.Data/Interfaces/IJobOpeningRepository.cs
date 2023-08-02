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
        /// <param name="jobOpening">The JobOpening object representing the job opening to be added.</param>
        void Add(JobOpening jobOpening);

        /// <summary>
        /// Updates an existing Job Opening in the database.
        /// </summary>
        /// <param name="jobOpening">The updated JobOpening object.</param>
        void Update(JobOpening jobOpening);

        /// <summary>
        /// Deletes an existing Job Opening from the database.
        /// </summary>
        /// <param name="job">The JobOpening object to be deleted.</param>
        public void Delete(JobOpening job);

        /// <summary>
        /// Retrieves the most recent job opening from the database.
        /// </summary>
        /// <returns>The most recent job opening.</returns>
        JobOpeningViewModel GetMostRecentJobOpening();
    }
}
