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
        private readonly IApplicantListRepository _applicantListRepository;
        private readonly IJobOpeningRepository _jobOpeningRepository;
        public PublicApplicationFormService(IPublicApplicationFormRepository repository, IMapper mapper,IApplicantListRepository applicantListRepository,IJobOpeningRepository jobOpeningRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _applicantListRepository= applicantListRepository;
            _jobOpeningRepository = jobOpeningRepository;
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
        /// <summary>
        /// This function combines three tables which are Applicant, JobOpening and PublicApplicationForm.
        /// </summary>
        /// <param name="applicantId"></param>
        /// <param name="jobId"></param>
        /// <returns>An object reference containg the Applicant's Public Application details</returns>
        public ApplicantDetails GetApplicationFormById(int applicantId, int jobId)
        {
            var applicant = _applicantListRepository.GetById(applicantId);
            var job = _jobOpeningRepository.GetById(jobId);
            var form = _repository.GetById(applicantId);
            //This new variable combines the three tables instances to view accurately the applicant's public application form.
            var data = new ApplicantDetails
            {
                ApplicantId = applicant.Id,
                FirstName = applicant.Firstname,
                LastName = applicant.Lastname,
                Email = applicant.EmailAddress,
                Address = form.Address,
                Position = job.Position,
                EmploymentType = job.JobType,
                PhoneNumber = form.PhoneNumber,
                School = form.School,
                SchoolDepartment = form.SchoolDepartment,
                Achievements = form.Achievements,
                ReferenceOneFullName = form.ReferenceOneFullName,
                RelationshipOne = form.RelationshipOne,
                ContactInfoOne = form.ContactInfoOne,
                ReferenceTwoFullName = form.ReferenceTwoFullName,
                RelationshipTwo = form.RelationshipTwo,
                ContactInfoTwo = form.ContactInfoTwo,
                ReferenceThreeFullName = form.ReferenceThreeFullName,
                RelationshipThree = form.RelationshipThree,
                ContactInfoThree = form.ContactInfoThree
            };

            return data;
        }
    }
}
