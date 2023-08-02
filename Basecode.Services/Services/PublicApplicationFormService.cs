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
        private readonly IApplicantListRepository _applicantListRepository;
        private readonly IJobOpeningRepository _jobOpeningRepository;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        
        public PublicApplicationFormService(IPublicApplicationFormRepository repository, IMapper mapper,IApplicantListRepository applicantListRepository,IJobOpeningRepository jobOpeningRepository) 
        {
            _repository = repository;
            _mapper = mapper;
            _applicantListRepository= applicantListRepository;
            _jobOpeningRepository = jobOpeningRepository;
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
        public PublicApplicationFormViewModel GetByApplicationId(int id)
        {
            var data = (PublicApplicationForm)_repository.GetByApplicationId(id);
            return _mapper.Map<PublicApplicationFormViewModel>(data);
        }

        public void Responded(int id, int trigger)
        {
            var Id = _applicantListRepository.GetById(id).FormId;
            _repository.Responded((int)Id, trigger);
        }

        public int CountResponded(int id)
        {
            try
            {
                var Id = _applicantListRepository.GetById(id);
                var form = _repository.GetByApplicationId(Id.FormId);
                var count = form.AnsweredOne;

                // Log successful count of responded forms
                _logger.Info($"Count of responded forms with ID: {id} is {count}");

                return (int)count;
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the counting process
                _logger.Error(ex, $"Error occurred while counting responded forms with ID: {id}");
                throw;
            }
        }
        /// <summary>
        /// This function combines three tables which are Applicant, JobOpening and PublicApplicationForm.
        /// </summary>
        /// <param name="applicantId"></param>
        /// <param name="jobId"></param>
        /// <returns>An object reference containg the Applicant's Public Application details</returns>
        public ApplicantDetails GetApplicationFormById(int applicantId, int jobId)
        {
            var applicant = _applicantListRepository.GetByFormId(applicantId);
            var job = _jobOpeningRepository.GetById(jobId);
            var form = _repository.GetByApplicationId(applicant.FormId);
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
                ContactInfoThree = form.ContactInfoThree,
                ApplicationID = applicant.FormId,
            };

            return data;
        }
    }
}
