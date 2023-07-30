using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;
using System.Linq;

namespace Basecode.Data.Repositories
{
    public class ApplicationTrackingRepository
    {
        private readonly BasecodeContext _context;

        public ApplicationTrackingRepository(BasecodeContext context)
        {
            _context = context;
        }

        public Applicant GetApplicationTracking(int applicantId)
        {
            // Retrieve the ApplicationTracking model from the database based on the applicantId
            return _context.Applicant.FirstOrDefault(a => a.FormID == applicantId);
        }
    }
}
