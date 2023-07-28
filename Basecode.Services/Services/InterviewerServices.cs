using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Services.Interfaces;
using AutoMapper;
using NLog;

namespace Basecode.Services.Services
{
    public class InterviewerServices: IInterviewerServices
    {
        private readonly IInterviewerRepository _interviewerRepository;
        private readonly IMapper _mapper;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public InterviewerServices(IInterviewerRepository interviewerRepository,IMapper mapper)
        {
            _interviewerRepository = interviewerRepository;
            _mapper = mapper;
        }

        public void Add(Interviewer interviewer)
        {
            try
            {
                interviewer.CreatedBy = System.Environment.UserName;
                interviewer.UpdatedBy = System.Environment.UserName;
                interviewer.CreatedTime = DateTime.Now;
                interviewer.UpdatedTime = DateTime.Now;

                _interviewerRepository.Add(interviewer);

                // Log successful interviewer addition
                _logger.Info("Interviewer added successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the interviewer addition process
                _logger.Error(ex, "Error occurred while adding interviewer.");
                throw;
            }
        }

        public List<InterviewerViewModel> GetAll() 
        {
            try
            {
                var interviewers = _interviewerRepository.GetAll().Select(s => new InterviewerViewModel
                {
                    InterviewerId = s.InterviewerId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    ContactNo = s.ContactNo,
                }).ToList();

                // Log successful retrieval of interviewers
                _logger.Info($"Retrieved {interviewers.Count} interviewers.");

                return interviewers;
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the retrieval process
                _logger.Error(ex, "Error occurred while retrieving interviewers.");
                throw;
            }
        }

        public void Update(Interviewer interviewer) 
        {
            try
            {
                var data = _interviewerRepository.GetById(interviewer.InterviewerId);
                data.FirstName = interviewer.FirstName;
                data.LastName = interviewer.LastName;
                data.Email = interviewer.Email;
                data.ContactNo = interviewer.ContactNo;
                data.UpdatedBy = System.Environment.UserName;
                data.UpdatedTime = DateTime.Now;

                _interviewerRepository.Update(data);

                // Log successful interviewer update
                _logger.Info($"Interviewer {interviewer.InterviewerId} updated successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the interviewer update process
                _logger.Error(ex, $"Error occurred while updating interviewer {interviewer.InterviewerId}.");
                throw;
            }
        }

        public InterviewerViewModel GetById(int id)
        {
            try
            {
                var data = _interviewerRepository.GetById(id);

                // Log successful retrieval of the interviewer by ID
                _logger.Info($"Retrieved interviewer with ID: {id}");

                return _mapper.Map<InterviewerViewModel>(data);
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the retrieval process
                _logger.Error(ex, $"Error occurred while retrieving interviewer with ID: {id}");
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var interviewer = _interviewerRepository.GetById(id);
                _interviewerRepository.Delete(interviewer);

                // Log successful deletion of the interviewer
                _logger.Info($"Deleted interviewer with ID: {id}");
            }
            catch (Exception ex)
            {
                // Log the exception if any occurs during the deletion process
                _logger.Error(ex, $"Error occurred while deleting interviewer with ID: {id}");
                throw;
            }
        }
    }
}
