using Basecode.Data.Models;
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
    }
}
