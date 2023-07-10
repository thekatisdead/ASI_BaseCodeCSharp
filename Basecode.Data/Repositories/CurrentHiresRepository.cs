using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;
using System.Linq;

namespace Basecode.Data.Repositories
{
    public class CurrentHiresRepository
    {
        private readonly BasecodeContext _context;

        public CurrentHiresRepository(BasecodeContext context)
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
