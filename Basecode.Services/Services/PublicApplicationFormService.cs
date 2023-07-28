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
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

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

                _logger.Info("Added a new public application form with ID {formId}.", applicationForm.Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while adding a new public application form: {errorMessage}", ex.Message);
                throw;
            }
        }

        public PublicApplicationFormViewModel GetById(int id)
        {
            try
            {
                var data = (PublicApplicationForm)_repository.GetById(id);
                return _mapper.Map<PublicApplicationFormViewModel>(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while retrieving public application form with ID {formId}: {errorMessage}", id, ex.Message);
                throw;
            }
        }

        public int CountResponded(int id)
        {
            try
            {
                return _repository.CountResponded(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while counting responded public application forms for ID {formId}: {errorMessage}", id, ex.Message);
                throw;
            }
        }
    }
}