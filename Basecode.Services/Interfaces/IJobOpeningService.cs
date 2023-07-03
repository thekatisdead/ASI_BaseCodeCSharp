using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface IJobOpeningService
    {
        /// <summary>
        /// Retrieve all Job Openings from the database
        /// </summary>
        /// <returns>A list of JobOpeningViewModel object representing all Job Openings.</returns>
        List<JobOpeningViewModel> RetrieveAll();

        /// <summary>
        /// Retrieves a Job Opening by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a JobOpeningViewModel object representing a Job Opening</returns>
        JobOpeningViewModel GetById(int id);

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
        /// <summary>
        /// Deletes a selected Job Opening
        /// </summary>
        /// <param name="jobOpening"></param>
        public void Delete(int id);

	}
}
