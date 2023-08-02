using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Interfaces
{
    public interface IApplicantListRepository
    {
        /// <summary>
        /// Retrieves all applicants from the database.
        /// </summary>
        /// <returns>An IQueryable of Applicant containing all applicants.</returns>
        IQueryable<Applicant> RetrieveAll();

        /// <summary>
        /// Adds a new applicant to the database.
        /// </summary>
        /// <param name="applicant">The Applicant object representing the applicant to be added.</param>
        public void Add(Applicant applicant);

        /// <summary>
        /// Retrieves an applicant from the database by their FormId.
        /// </summary>
        /// <param name="id">The FormId of the applicant to retrieve.</param>
        /// <returns>The Applicant entity with the specified FormId if found; otherwise, returns null.</returns>
        public Applicant GetByFormId(int id);

        void UpdateStatus(int applicantId, string status);

        /// <summary>
        /// Proceeds an applicant to a new step in the application process.
        /// </summary>
        /// <param name="applicantId">The ID of the applicant to proceed.</param>
        /// <param name="step">The new step to set for the applicant.</param>
        void ProceedTo(int applicantId, string step);

        /// <summary>
        /// Retrieves an applicant by their ID.
        /// </summary>
        /// <param name="id">The ID of the applicant to retrieve.</param>
        /// <returns>The Applicant object.</returns>
        Applicant GetById(int id);

        /// <summary>
        /// Updates an existing applicant in the database.
        /// </summary>
        /// <param name="applicant">The updated Applicant object.</param>
        public void Update(Applicant applicant);

        /// <summary>
        /// Retrieves the most recent applicant's info from the database.
        /// </summary>
        /// <returns>The most recent applicant's info.</returns>
        ApplicantListViewModel GetMostRecentApplicant();

        /// <summary>
        /// Retrieves the most recent applicant from the database for requirements.
        /// </summary>
        /// <returns>
        /// The ApplicantListViewModel representing the most recent applicant for requirements if found;
        /// otherwise, returns a default ApplicantListViewModel with N/A values.
        /// </returns>
        ApplicantListViewModel GetMostRecentApplicantForRequirements();

        public void UpdateConfirmed(int applicantId, string status);
    }
}
