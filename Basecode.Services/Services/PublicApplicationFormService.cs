using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Services.Services.ErrorHandling;

namespace Basecode.Services.Services
{
    public class PublicApplicationFormService : IPublicApplicationFormService
    {
        private readonly IPublicApplicationFormRepository _repository;
        private readonly IMapper _mapper;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public PublicApplicationFormService(IPublicApplicationFormRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void AddForm(PublicApplicationFormViewModel applicationForm)
        {
            try
            {
                applicationForm.CreatedTime = DateTime.Now;
                applicationForm.CreatedBy = System.Environment.UserName;

                _repository.AddForm(_mapper.Map<PublicApplicationForm>(applicationForm));

                // Log successful addition of the form
                _logger.Info("Public application form added successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the addition process
                _logger.Error(ex, "Error occurred while adding public application form.");
                throw;
            }
        }

        public PublicApplicationFormViewModel GetById(int id)
        {
            try
            {
                var data = (PublicApplicationForm)_repository.GetById(id);

                // Log successful retrieval of the form by ID
                _logger.Info($"Retrieved public application form with ID: {id}");

                return _mapper.Map<PublicApplicationFormViewModel>(data);
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the retrieval process
                _logger.Error(ex, $"Error occurred while retrieving public application form with ID: {id}");
                throw;
            }
        }

        public int CountResponded(int id)
        {
            try
            {
                var count = _repository.CountResponded(id);

                // Log successful count of responded forms
                _logger.Info($"Count of responded forms with ID: {id} is {count}");

                return count;
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the counting process
                _logger.Error(ex, $"Error occurred while counting responded forms with ID: {id}");
                throw;
            }
        }
    }
}
