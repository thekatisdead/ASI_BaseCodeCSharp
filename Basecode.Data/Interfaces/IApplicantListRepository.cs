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

        public void Add(Applicant applicant);
        public Applicant GetByFormId(int id);

        void UpdateStatus(int applicantId, string status);

        void ProceedTo(int applicantId, string step);

        /// <summary>
        /// Retrieves an applicant by their ID.
        /// </summary>
        /// <param name="id">The ID of the applicant to retrieve.</param>
        /// <returns>The Applicant object.</returns>
        Applicant GetById(int id);
        public void Update(Applicant applicant);

        /// <summary>
        /// Retrieves the most recent applicant's info from the database.
        /// </summary>
        /// <returns>The most recent applicant's info.</returns>
        ApplicantListViewModel GetMostRecentApplicant();
        ApplicantListViewModel GetMostRecentApplicantForRequirements();

        public void UpdateConfirmed(int applicantId, string status);
    }
}
