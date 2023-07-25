using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Services;
using Microsoft.Graph.Beta.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule = Basecode.Data.Models.Schedule;

namespace Basecode.Test.Services
{
    public class ScheduleServiceTests
    {
        private readonly ScheduleService _scheduleService;
        private readonly Mock<IScheduleRepository> _scheduleRepository;
        private readonly Mock<IJobOpeningRepository> _jobOpeningRepository;
        private readonly Mock<IInterviewerRepository> _interviewerRepository;
        private readonly Mock<IApplicantListRepository> _applicantListRepository;
        private readonly IMapper _mapper;

        public ScheduleServiceTests()
        {
            _scheduleRepository = new Mock<IScheduleRepository>();
            _jobOpeningRepository = new Mock<IJobOpeningRepository>();
            _interviewerRepository = new Mock<IInterviewerRepository>();
            _applicantListRepository = new Mock<IApplicantListRepository>();
            _scheduleService = new ScheduleService(_scheduleRepository.Object, _jobOpeningRepository.Object, _interviewerRepository.Object, _applicantListRepository.Object, _mapper);
        }

        [Fact]
        public void Add_ValidSchedule_ShouldReturnNone()
        {
            // Arrange
            var schedule = new Schedule
            {
                ScheduleId = 1,
                InterviewerId = 1,
                JobId = 1,
                Instruction = "Test"
            };

            // Act
            _scheduleService.Add(schedule);

            // Assert
            _scheduleRepository.Verify(r => r.Add(It.IsAny<Schedule>()), Times.Once);
        }

        [Fact]
        public void Add_InvalidSchedule_ShouldReturnNone()
        {
            // Arrange
            var schedule = new Schedule();

            // Act
            _scheduleService.Add(schedule);

            // Assert 
            _scheduleRepository.Verify(r => r.Add(It.IsAny<Schedule>()), Times.Once);
        }

        [Fact]
        public void GetAll_ListofSchedule_ShouldReturnAllSchedules()
        {
            // Arrange
            var schedules = new List<Schedule>
            {
                new Schedule
                {
                    ScheduleId = 1,
                    InterviewerId = 1,
                    JobId = 1,
                    Instruction = "Test"
                },
                new Schedule
                {
                    ScheduleId = 2,
                    InterviewerId = 2,
                    JobId = 2,
                    Instruction = "Test2"
                }
            };

            _scheduleRepository.Setup(r => r.GetAll()).Returns(schedules.AsQueryable());

            // Act
            var result = _scheduleService.GetAll();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetAll_EmptyListofSchedule_ReturnsEmpty()
        {
            // Arrange
            var schedules = new List<Schedule>();

            _scheduleRepository.Setup(r => r.GetAll());

            // Act
            var result = _scheduleService.GetAll();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetDetails_ValidScheduleDetails_ReturnsListOfScheduleDetails()
        {
            // Arrange
            // Setup fake data for the repositories
            var interviewers = new List<Interviewer>
            {
                new Interviewer { InterviewerId = 1, FirstName = "John", LastName = "Doe" },
                new Interviewer { InterviewerId = 2, FirstName = "Jane", LastName = "Smith" }
                // Add more interviewers as needed
            };

            var jobOpenings = new List<JobOpening>
            {
                new JobOpening { Id = 1, Position = "Software Developer" },
                new JobOpening { Id = 2, Position = "QA Engineer" }
                // Add more job openings as needed
            };

            var schedules = new List<Schedule>
            {
                new Schedule { ScheduleId = 1, InterviewerId = 1, JobId = 1, StartTime = "9:00 AM", EndTime = "10:00 AM", Date = "2023-07-15", Instruction = "Test" },
                new Schedule { ScheduleId = 2, InterviewerId = 2, JobId = 2, StartTime = "1:00 PM", EndTime = "2:00 PM", Date = "2023-07-16", Instruction = "Test 2" }
                // Add more schedules as needed
            };

            _interviewerRepository.Setup(r => r.GetAll()).Returns(interviewers.AsQueryable());
            _jobOpeningRepository.Setup(r => r.RetrieveAll()).Returns(jobOpenings.AsQueryable());
            _scheduleRepository.Setup(r => r.GetAll()).Returns(schedules.AsQueryable());

            // Act
            var result = _scheduleService.GetDetails();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetDetails_EmptyListOfScheduleDetails_ReturnsEmpty()
        {
            // Arrange\
            // Setup fake data for the repositories
            var interviewers = new List<Interviewer>();
            var jobOpenings = new List<JobOpening>();
            var schedules = new List<Schedule>();

            _interviewerRepository.Setup(r => r.GetAll()).Returns(interviewers.AsQueryable());
            _jobOpeningRepository.Setup(r => r.RetrieveAll()).Returns(jobOpenings.AsQueryable());
            _scheduleRepository.Setup(r => r.GetAll()).Returns(schedules.AsQueryable());

            // Act
            var result = _scheduleService.GetDetails();

            // Assert
            Assert.Empty(result);
        }

        //[Fact]
        //public void GetById_ValidId_ReturnsScheduleViewModel()
        //{
        //    // Arrange
        //    var id = 1;

        //    var schedule1 = new Schedule
        //    {
        //        ScheduleId = 1,
        //        InterviewerId = 1,
        //        JobId = 1,
        //        Instruction = "Test"
        //    };

        //    var schedule = new ScheduleViewModel
        //    {
        //        ScheduleId = 1,
        //        JobOpenings = new List<JobOpeningViewModel>
        //        {
        //            new JobOpeningViewModel
        //            {
        //                Id = id,
        //                Position = "Software Developer",
        //                JobType = "Hello World",
        //                Salary = 100000,
        //                Hours = 40,
        //                Shift = "Morning",
        //                Description = "Test",
        //            }
        //        },
        //        Interviewers = new List<InterviewerViewModel>
        //        {
        //            new InterviewerViewModel
        //            {
        //                InterviewerId = 1,
        //                FirstName = "Ella",
        //                LastName = "Abueva",
        //                Email = "ella@gmail.com",
        //                ContactNo = "09995067663"
        //            }
        //        },
        //        InterviewerId = 1,
        //        JobId = 1,
        //        Instruction = "Test"
        //    };

        //    _scheduleRepository.Setup(r => r.GetById(id)).Returns(schedule1);

        //    // Act
        //    var result = _scheduleService.GetById(id);
            
        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<ScheduleViewModel>(result);
        //}

        //[Fact]
        //public void GetById_InvalidId_ReturnsScheduleViewModel()
        //{
        //    // Arrange
        //    var id = 0;

        //    var schedule1 = new Schedule();

        //    var schedule = new ScheduleViewModel();

        //    _scheduleRepository.Setup(r => r.GetById(id)).Returns(schedule1);

        //    // Act
        //    var result = _scheduleService.GetById(id);

        //    // Assert
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public void UpdateSchedule_ValidSchedule_ReturnsNone()
        //{
        //    // Arrange
        //    var schedule = new Schedule
        //    {
        //        ScheduleId = 1,
        //        InterviewerId = 1,
        //        JobId = 1,
        //        Instruction = "Test"
        //    };

        //    // Act
        //    _scheduleService.UpdateSchedule(schedule);

        //    // Assert
        //    _scheduleRepository.Verify(r => r.UpdateSchedule(It.IsAny<Schedule>()), Times.Never);
        //}

        //[Fact]
        //public void UpdateSchedule_InvalidSchedule_ShouldReturnNone()
        //{
        //    // Arrange
        //    var schedule = new Schedule();

        //    // Act
        //    _scheduleService.UpdateSchedule(schedule);

        //    // Assert 
        //    _scheduleRepository.Verify(r => r.UpdateSchedule(It.IsAny<Schedule>()), Times.Never);
        //}

        [Fact]
        public void DeleteSchedule_ValidSchedule_ReturnsNone()
        {
            // Arrange
            var schedule = new Schedule
            {
                ScheduleId = 1,
                InterviewerId = 1,
                JobId = 1,
                Instruction = "Test"
            };

            // Act
            _scheduleService.DeleteSchedule(schedule.ScheduleId);

            // Assert
            _scheduleRepository.Verify(r => r.DeleteSchedule(It.IsAny<Schedule>()), Times.Once);
        }

        [Fact]
        public void DeleteSchedule_InvalidSchedule_ShouldReturnNone()
        {
            // Arrange
            var schedule = new Schedule();

            // Act
            _scheduleService.DeleteSchedule(schedule.ScheduleId);

            // Assert 
            _scheduleRepository.Verify(r => r.DeleteSchedule(It.IsAny<Schedule>()), Times.Once);
        }

        [Fact]
        public void GetApplicantListAccordingToJobApplied_HasApplicantList_ReturnListofApplicantList()
        {
            // Arrange
            var jobId = 1;
            var applicantlist = new List<ApplicantListViewModel>
            {
                new ApplicantListViewModel
                {
                    Id = 1,
                    Firstname = "Ella",
                    Lastname = "Abueva",
                    Tracker = "Test",
                    Grading = "A",
                    JobApplied = 2,
                    EmailAddress = "email@gmail.com"
                }
            };

            _scheduleService.GetApplicantListAccordingToJobApplied(jobId);

            // Act
            var result = _scheduleService.GetApplicantListAccordingToJobApplied(jobId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ApplicantListViewModel>>(result);
        }

        [Fact]
        public void GetApplicantListAccordingToJobApplied_IsEmpty_ReturnListofApplicantList()
        {
            // Arrange
            var jobId = 1;
            var applicantlist = new List<ApplicantListViewModel>();

            _scheduleService.GetApplicantListAccordingToJobApplied(jobId);

            // Act
            var result = _scheduleService.GetApplicantListAccordingToJobApplied(jobId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
