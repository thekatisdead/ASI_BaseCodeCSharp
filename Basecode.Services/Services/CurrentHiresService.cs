using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Services.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class CurrentHiresService : ICurrentHiresService
    {
        private readonly ICurrentHiresRepository _repository;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public CurrentHiresService(ICurrentHiresRepository repository)
        {
            _repository = repository;
        }

        public void AddHire(int applicantId, int jobId)
        {
            try
            {
                _repository.AddHire(applicantId, jobId);

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
