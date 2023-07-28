﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
using NLog;

namespace Basecode.Data.Repositories
{
    public class ApplicationTrackingRepository
    {
        private readonly BasecodeContext _context;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public ApplicationTrackingRepository(BasecodeContext context)
        {
            _context = context;
        }

        public Applicant GetApplicationTracking(int applicantId)
        {
            try
            {
                // Retrieve the ApplicationTracking model from the database based on the applicantId
                Applicant applicant = _context.Applicant.FirstOrDefault(a => a.Id == applicantId);

                if (applicant != null)
                {
                    // Log the successful retrieval of the model
                    _logger.Trace($"ApplicationTracking retrieved for applicantId: {applicantId}");
                }
                else
                {
                    // Log a message if the applicant is not found
                    _logger.Trace($"ApplicationTracking not found for applicantId: {applicantId}");
                }

                return applicant;
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs
                _logger.Error(ex, $"Error occurred while retrieving ApplicationTracking for applicantId: {applicantId}");
                throw;
            }
        }
    }
}
