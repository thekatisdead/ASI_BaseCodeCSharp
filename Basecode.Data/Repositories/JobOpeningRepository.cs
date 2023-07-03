using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class JobOpeningRepository : BaseRepository, IJobOpeningRepository
    {
        /// <summary>
        /// Creates an instance of Basecode context
        /// </summary>
        private readonly BasecodeContext _context;
        public JobOpeningRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieve all Job Openings from the database
        /// </summary>
        /// <returns>An IQueryable of Job Openings.</returns>
        public IQueryable<JobOpening> RetrieveAll()
        {
            return this.GetDbSet<JobOpening>();
        }

        /// <summary>
        /// Retrieves a Job Opening by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Job Opening</returns>
        public JobOpening GetById(int id) 
        {
            return _context.JobOpening.Find(id);
        }

        /// <summary>
        /// Create/Add a new Job Opening
        /// </summary>
        /// <param name="jobOpening"></param>
        public void Add(JobOpening jobOpening)
        {
            _context.JobOpening.Add(jobOpening);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates a selected Job Opening
        /// </summary>
        /// <param name="jobOpening"></param>
        public void Update(JobOpening jobOpening)
        {
            _context.JobOpening.Update(jobOpening);
            _context.SaveChanges();
        }
    }
}
