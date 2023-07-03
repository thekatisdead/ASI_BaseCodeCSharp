using Basecode.Data.Models;
using Basecode.Data.ViewModels;
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
        /// Retrieves all applicants from the repository.
        /// </summary>
        /// <returns>A list of ApplicantListViewModel objects representing all applicants.</returns>
        List<ApplicantListViewModel> RetrieveAll();
    }
}
