using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class ApplicantListRepository: BaseRepository, IApplicantListRepository
    {
        public readonly BasecodeContext _context;

        public ApplicantListRepository(IUnitOfWork unitOfWork, BasecodeContext context): base(unitOfWork) 
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all applicants from the database.
        /// </summary>
        /// <returns>An IQueryable of Applicant containing all applicants.</returns>
        public IQueryable<Applicant> RetrieveAll()
        {
            return this.GetDbSet<Applicant>();
        }

        public Applicant GetById(int id)
        {
            // warning here is that it is possible that the return below
            // will return a null value
            return _context.Applicant.FirstOrDefault(a => a.Id == id);
        }
        public void Update(Applicant applicant)
        {
            _context.Update(applicant);
            _context.SaveChanges();
        }

    }
}
