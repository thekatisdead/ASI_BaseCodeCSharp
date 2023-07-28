using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;
using System.Linq;
using Basecode.Data.Interfaces;
using NLog;

namespace Basecode.Data.Repositories
{
    public class CurrentHiresRepository : BaseRepository, ICurrentHiresRepository
    {
        private readonly BasecodeContext _context;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public CurrentHiresRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork) 
        {
            _context = context;
        }

        public void AddHire(int applicantId, int jobId)
        {
            try
            {
                var hire = new CurrentHires
                {
                    ApplicantID = applicantId,
                    JobID = jobId
                };

                _context.CurrentHires.Add(hire);
                _context.SaveChanges();

                // Log successful hire addition
                _logger.Info($"Hire added successfully for applicantId: {applicantId}, jobId: {jobId}");
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the hire addition process
                _logger.Error(ex, $"Error occurred while adding hire for applicantId: {applicantId}, jobId: {jobId}");
                throw;
            }
        }
    }
}
