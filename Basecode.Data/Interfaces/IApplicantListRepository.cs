using Basecode.Data.Models;
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

        void UpdateStatus(int applicantId, string status);

        void ProceedTo(int applicantId, string step);

        Applicant GetById(int id);
    }
}
