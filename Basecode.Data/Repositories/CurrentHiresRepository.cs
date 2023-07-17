using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;
using System.Linq;
using Basecode.Data.Interfaces;

namespace Basecode.Data.Repositories
{
    public class CurrentHiresRepository : BaseRepository, ICurrentHiresRepository
    {
        private readonly BasecodeContext _context;

        public CurrentHiresRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork) 
        {
            _context = context;
        }

        public void AddHire(int applicantId, int jobId)
        {
            var hire = new CurrentHires
            {
                ApplicantID = applicantId,
                JobID = jobId
            };

            _context.CurrentHires.Add(hire);
            _context.SaveChanges();
        }
    }
}
