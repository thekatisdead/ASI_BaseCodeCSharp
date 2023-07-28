using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class PublicApplicationFormRepository : BaseRepository, IPublicApplicationFormRepository
    {
        private readonly BasecodeContext _context;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public PublicApplicationFormRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public void AddForm(PublicApplicationForm applicationForm)
        {
            _context.PublicApplicationForm.Add(applicationForm);
            _context.SaveChanges();
        }
        public PublicApplicationForm GetByApplicantId(int id)
        {
            return _context.PublicApplicationForm.FirstOrDefault(p =>p.ApplicantId == id);
        }
        public PublicApplicationForm GetById(int id)
        {

          try
            {
                var form = _context.PublicApplicationForm.FirstOrDefault(p => p.ApplicantId == id);
                _logger.Info($"Form retrieved successfully for ID: {id}");

                if (form == null)
                {
                    _logger.Info($"Form not found for ID: {id}");
                }

                return form;
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the form retrieval process
                _logger.Error(ex, $"Error occurred while retrieving form for ID: {id}");
                throw;
            }
        }

        public int CountResponded(int id)
        {
            try
            {
                var _application = this.GetById(id);
                var total = 0;
                if (_application.AnsweredOne != null)
                {
                    total++;
                }
                if (_application.AnsweredTwo != null)
                {
                    total++;
                }
                if (_application.AnsweredThree != null)
                {
                    total++;
                }

                // Log the successful count
                _logger.Info($"CountResponded: Counted {total} responses for ID: {id}");

                return total;
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the count process
                _logger.Error(ex, $"Error occurred while counting responses for ID: {id}");
                throw;
            }

        }
    }
}
