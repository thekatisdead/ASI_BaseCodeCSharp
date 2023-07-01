using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class ApplicantListService: IApplicantListService
    {
        private readonly IApplicantListRepository _repository;

        public ApplicantListService(IApplicantListRepository repository) 
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all applicants.
        /// </summary>
        /// <returns>A list of Applicant objects representing all applicants.</returns>
        public List<Applicant> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }
    }
}
