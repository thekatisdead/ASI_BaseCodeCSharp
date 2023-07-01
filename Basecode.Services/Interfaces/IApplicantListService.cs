using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface IApplicantListService
    {
        /// <summary>
        /// Retrieves all applicants.
        /// </summary>
        /// <returns>A list of Applicant objects representing all applicants.</returns>
        List<Applicant> RetrieveAll();
    }
}
