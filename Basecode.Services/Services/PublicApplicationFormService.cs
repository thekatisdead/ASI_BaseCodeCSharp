using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
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
        public PublicApplicationFormService(IPublicApplicationFormRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void AddForm(PublicApplicationFormViewModel applicationForm)
        {
            applicationForm.CreatedTime = DateTime.Now;
            applicationForm.CreatedBy = System.Environment.UserName;

            _repository.AddForm(_mapper.Map<PublicApplicationForm>(applicationForm));
        }

        public PublicApplicationFormViewModel GetById(int id)
        {
            var data = (PublicApplicationForm)_repository.GetById(id);
            return _mapper.Map<PublicApplicationFormViewModel>(data);
        }
    }
}
