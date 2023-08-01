using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Basecode.Test.Controllers
{
    public class HrSchedulerControllerTests
    {
        private readonly HrSchedulerController _controller;
        private readonly Mock<IJobOpeningService> _jobOpeningService;
        private readonly Mock<IInterviewerServices> _interviewerService;
        private readonly Mock<IScheduleService> _scheduleService;
        private readonly Mock<IEmailSenderService> _emailSenderService;
        private readonly Mock<IUserService> _userService;

        public HrSchedulerControllerTests()
        {
            _jobOpeningService = new Mock<IJobOpeningService>();
            _interviewerService = new Mock<IInterviewerServices>();
            _scheduleService = new Mock<IScheduleService>();
            _emailSenderService = new Mock<IEmailSenderService>();
            _userService = new Mock<IUserService>();
            _controller = new HrSchedulerController(_interviewerService.Object, _jobOpeningService.Object, _scheduleService.Object, _emailSenderService.Object, _userService.Object);
        }

        [Fact]
        public void AddInterviewer_ReturnsViewResult()
        {
            // Act
            var result = _controller.AddInterviewer();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName); // The view name is not explicitly set, so it should be null.
        }

        [Fact]
        public void Schedule_WithValidHrScheduler_RedirectsToIndex()
        {
            // Arrange
            var hrScheduler = new HrScheduler(); // Create a valid HrScheduler object
            _emailSenderService.Setup(x => x.SendEmailInterviewSchedule(hrScheduler));

            // Act
            var result = _controller.Schedule(hrScheduler);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Schedule_WithNullHrScheduler_RedirectsToIndex()
        {
            // Arrange
            HrScheduler hrScheduler = null; // HrScheduler object is null

            // Act
            var result = _controller.Schedule(hrScheduler);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Home_HasScheduleDetails_ReturnsViewWithSchedules()
        {
            // Arrange
            var schedules = new List<ScheduleDetails>
            {
                new ScheduleDetails { ScheduleId = 1, Date = DateTime.Now.ToString(), InterviewerId = 1, JobId = 2, StartTime = "09:00 AM", EndTime = "11:00 AM" },
                new ScheduleDetails { ScheduleId = 2, Date = DateTime.Now.AddDays(1).ToString(), InterviewerId = 2, JobId = 3, StartTime = "01:00 PM", EndTime = "03:00 PM" },
            };

            _scheduleService.Setup(x => x.GetDetails()).Returns(schedules);

            // Act
            var result = _controller.home();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ScheduleDetails>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count()); // Assuming there are two schedules in the mock data.
        }

        [Fact]
        public void Home_HasNoScheduleDetails_ReturnsViewWithNoSchedules()
        {
            // Arrange
            List<ScheduleDetails> schedules = null; // Simulating no schedules returned from the service.
            _scheduleService.Setup(x => x.GetDetails()).Returns(schedules);

            // Act
            var result = _controller.home();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.ViewData.Model;
            Assert.Null(model); // Assert that the model is null when there are no schedules.
        }

        [Fact]
        public void Add_SuccessfulAdd_RedirectsToInterviewerList()
        {
            // Arrange
            var interviewer = new Interviewer
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                ContactNo = "1234567890"
            };

            // Mock the Add method of IInterviewerServices
            _interviewerService.Setup(x => x.Add(interviewer)).Verifiable();

            // Act
            var result = _controller.Add(interviewer);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("InterviewerList", redirectToActionResult.ActionName);
            Assert.Equal("HrScheduler", redirectToActionResult.ControllerName); // Check the expected ControllerName
            _interviewerService.Verify(); // Verify that the Add method was called
        }

        [Fact]
        public void Add_UnsuccessfulAdd_ThrowsException()
        {
            // Arrange
            var interviewer = new Interviewer
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                ContactNo = "1234567890"
            };

            // Mock the Add method of IInterviewerServices to throw an exception, simulating unsuccessful add
            _interviewerService.Setup(x => x.Add(interviewer)).Throws(new Exception());

            // Act and Assert
            Assert.Throws<Exception>(() => _controller.Add(interviewer));
        }

        [Fact]
        public void InterviewerList_HasInterviewers_ReturnsViewWithInterviewers()
        {
            // Arrange
            var interviewers = new List<InterviewerViewModel>
            {
                new InterviewerViewModel { InterviewerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", ContactNo = "1234567890" },
                new InterviewerViewModel { InterviewerId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", ContactNo = "9876543210" }
            };

            _interviewerService.Setup(x => x.GetAll()).Returns(interviewers);

            // Act
            var result = _controller.InterviewerList();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<InterviewerViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count); // Assuming there are two interviewers in the mock data.
        }

        [Fact]
        public void InterviewerList_HasNoInterviewers_ReturnsViewWithNoInterviewers()
        {
            // Arrange
            List<InterviewerViewModel> interviewers = null; // Simulating no interviewers returned from the service.
            _interviewerService.Setup(x => x.GetAll()).Returns(interviewers);

            // Act
            var result = _controller.InterviewerList();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.ViewData.Model;
            Assert.Null(model); // Assert that the model is null when there are no interviewers.
        }

        [Fact]
        public void UpdateInterviewer_InterviewerExists_ReturnsViewWithInterviewer()
        {
            // Arrange
            int interviewerId = 1;
            var interviewer = new InterviewerViewModel
            {
                InterviewerId = interviewerId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                ContactNo = "1234567890"
            };

            _interviewerService.Setup(x => x.GetById(interviewerId)).Returns(interviewer);

            // Act
            var result = _controller.UpdateInterviewer(interviewerId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<InterviewerViewModel>(viewResult.ViewData.Model);
            Assert.Equal(interviewerId, model.InterviewerId);
            Assert.Equal(interviewer.FirstName, model.FirstName);
            Assert.Equal(interviewer.LastName, model.LastName);
            Assert.Equal(interviewer.Email, model.Email);
            Assert.Equal(interviewer.ContactNo, model.ContactNo);
        }

        [Fact]
        public void UpdateInterviewer_InterviewerNotFound_ReturnsNotFound()
        {
            // Arrange
            int interviewerId = 1;
            InterviewerViewModel interviewer = null; // Simulate no interviewer found
            _interviewerService.Setup(x => x.GetById(interviewerId)).Returns(interviewer);

            // Act
            var result = _controller.UpdateInterviewer(interviewerId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Update_ValidInterviewer_RedirectsToInterviewerList()
        {
            // Arrange
            var interviewer = new Interviewer
            {
                InterviewerId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                ContactNo = "1234567890"
            };

            // Act
            var result = _controller.Update(interviewer);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("InterviewerList", redirectToActionResult.ActionName);
            Assert.Equal("HrScheduler", redirectToActionResult.ControllerName);

            // Verify that the Update method was called with the correct interviewer object
            _interviewerService.Verify(x => x.Update(It.Is<Interviewer>(i => i.InterviewerId == interviewer.InterviewerId &&
                                                                                      i.FirstName == interviewer.FirstName &&
                                                                                      i.LastName == interviewer.LastName &&
                                                                                      i.Email == interviewer.Email &&
                                                                                      i.ContactNo == interviewer.ContactNo)), Times.Once);
        }

        [Fact]
        public void Update_InvalidInterviewer_ReturnsRedirectToActionResult()
        {
            // Arrange
            var interviewer = new Interviewer
            {
                InterviewerId = 1,
                FirstName = "", // An invalid interviewer with empty FirstName
                LastName = "Doe",
                Email = "john.doe@example.com",
                ContactNo = "1234567890"
            };

            // Act
            var result = _controller.Update(interviewer);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            _interviewerService.Verify(x => x.Update(It.IsAny<Interviewer>()), Times.Once);
        }

        [Fact]
        public void Delete_InterviewerExists_RedirectsToInterviewerList()
        {
            // Arrange
            int interviewerId = 1;
            _interviewerService.Setup(x => x.GetById(interviewerId)).Returns(new InterviewerViewModel());

            // Act
            var result = _controller.Delete(interviewerId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("InterviewerList", redirectToActionResult.ActionName);
            Assert.Equal("HrScheduler", redirectToActionResult.ControllerName);

            // Verify that the Delete method was called with the correct interviewer id
            _interviewerService.Verify(x => x.Delete(interviewerId), Times.Once);
        }

        [Fact]
        public void Delete_InterviewerNotFound_ReturnsNotFound()
        {
            // Arrange
            int interviewerId = 1;
            _interviewerService.Setup(x => x.GetById(interviewerId)).Returns((InterviewerViewModel)null);

            // Act
            var result = _controller.Delete(interviewerId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            // Verify that the Delete method was not called, as the interviewer was not found
            _interviewerService.Verify(x => x.Delete(interviewerId), Times.Never);
        }

        [Fact]
        public void ViewAddSchedule_HasSchedules_ReturnsViewWithScheduleViewModel()
        {
            // Arrange
            var jobOpenings = new List<JobOpeningViewModel>
            {
                new JobOpeningViewModel { Id = 1, Position = "Software Engineer", JobType = "Full-Time", Salary = 80000, Hours = 8, Shift = "Morning", Description = "Implementing Websites" },
                new JobOpeningViewModel { Id = 2, Position = "Data Analyst", JobType = "Part-Time", Salary = 60000, Hours = 9, Shift = "Evening", Description = "Analyzing Data"  }
            };

            var interviewers = new List<InterviewerViewModel>
            {
                new InterviewerViewModel { InterviewerId = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@email.com", ContactNo = "09876543219" },
                new InterviewerViewModel { InterviewerId = 2, FirstName = "Jane", LastName = "Smith", Email = "janesmith@email.com", ContactNo = "09546599219" }
            };

            _jobOpeningService.Setup(x => x.RetrieveAll()).Returns(jobOpenings);
            _interviewerService.Setup(x => x.GetAll()).Returns(interviewers);

            // Act
            var result = _controller.ViewAddSchedule();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ScheduleViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(model);
            Assert.Equal(jobOpenings.Count, model.JobOpenings.Count);
            Assert.Equal(interviewers.Count, model.Interviewers.Count);
        }

        [Fact]
        public void ViewAddSchedule_NullJobOpeningsAndInterviewers_ReturnsBadRequest()
        {
            // Arrange
            List<JobOpeningViewModel> jobOpenings = null; // Simulating null job openings
            _jobOpeningService.Setup(x => x.RetrieveAll()).Returns(jobOpenings);

            List<InterviewerViewModel> interviewers = null; // Simulating null interviewers
            _interviewerService.Setup(x => x.GetAll()).Returns(interviewers);

            // Act
            var result = _controller.ViewAddSchedule();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No job openings and interviewers available.", badRequestResult.Value);
        }

        [Fact]
        public void AddSchedule_SuccessfulAdd_RedirectsToHome()
        {
            // Arrange
            var schedule = new Schedule
            {
                InterviewerId = 1,
                JobId = 2,
                StartTime = "09:00 AM",
                EndTime = "11:00 AM",
                Date = "2023-07-15",
                Instruction = "Bring your resume and portfolio"
            };

            // Act
            var result = _controller.AddSchedule(schedule);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("home", redirectToActionResult.ActionName);
            Assert.Equal("HrScheduler", redirectToActionResult.ControllerName);

            // Verify that the Add method was called with the correct schedule object
            _scheduleService.Verify(x => x.Add(It.Is<Schedule>(s => s.InterviewerId == schedule.InterviewerId &&
                                                                   s.JobId == schedule.JobId &&
                                                                   s.StartTime == schedule.StartTime &&
                                                                   s.EndTime == schedule.EndTime &&
                                                                   s.Date == schedule.Date &&
                                                                   s.Instruction == schedule.Instruction)), Times.Once);
        }

        [Fact]
        public void AddSchedule_InvalidTimeFormat_ReturnsRedirectToHome()
        {
            // Arrange
            var schedule = new Schedule
            {
                InterviewerId = 1,
                JobId = 2,
                StartTime = "09:00", // Invalid format, should be "HH:mm"
                EndTime = "11:00 AM",
                Date = "2023-07-15",
                Instruction = "Bring your resume and portfolio"
            };

            // Act
            var result = _controller.AddSchedule(schedule);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("home", redirectToActionResult.ActionName);
            Assert.Equal("HrScheduler", redirectToActionResult.ControllerName);

            _scheduleService.Verify(x => x.Add(It.IsAny<Schedule>()), Times.Once);
        }

        [Fact]
        public void AddSchedule_InvalidDateFormat_ReturnsRedirectToHome()
        {
            // Arrange
            var schedule = new Schedule
            {
                InterviewerId = 1,
                JobId = 2,
                StartTime = "09:00 AM",
                EndTime = "11:00 AM",
                Date = "2023/07/15", // Invalid format, should be "yyyy-MM-dd"
                Instruction = "Bring your resume and portfolio"
            };

            // Act
            var result = _controller.AddSchedule(schedule);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("home", redirectToActionResult.ActionName);
            Assert.Equal("HrScheduler", redirectToActionResult.ControllerName);

            _scheduleService.Verify(x => x.Add(It.IsAny<Schedule>()), Times.Once);
        }

        [Fact]
        public void EditSchedule_ScheduleFound_ReturnsViewWithSchedule()
        {
            // Arrange
            int scheduleId = 1;
            var schedule = new ScheduleViewModel
            {
                ScheduleId = scheduleId,
                InterviewerId = 1,
                JobId = 1,
                StartTime = "10:00 AM",
                EndTime = "12:00 PM",
                Date = "2023-07-15",
                Instruction = "Some instructions"
            };

            _scheduleService.Setup(x => x.GetById(scheduleId)).Returns(schedule);
            _jobOpeningService.Setup(x => x.RetrieveAll()).Returns(new List<JobOpeningViewModel>());
            _interviewerService.Setup(x => x.GetAll()).Returns(new List<InterviewerViewModel>());

            // Act
            var result = _controller.EditSchedule(scheduleId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ScheduleViewModel>(viewResult.ViewData.Model);
            Assert.Equal(scheduleId, model.ScheduleId);
        }

        [Fact]
        public void EditSchedule_ScheduleNotFound_ReturnsNotFound()
        {
            // Arrange
            int scheduleId = 1;
            ScheduleViewModel schedule = null; // Simulating a schedule not found
            _scheduleService.Setup(x => x.GetById(scheduleId)).Returns(schedule);

            // Act
            var result = _controller.EditSchedule(scheduleId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UpdateSchedule_ValidSchedule_RedirectsToHome()
        {
            // Arrange
            var schedule = new Schedule
            {
                ScheduleId = 1,
                InterviewerId = 1,
                JobId = 1,
                StartTime = "10:00 AM",
                EndTime = "12:00 PM",
                Date = "2023-07-15",
                Instruction = "Some instructions"
            };

            _scheduleService.Setup(x => x.UpdateSchedule(It.IsAny<Schedule>()));

            // Act
            var result = _controller.UpdateSchedule(schedule);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("home", redirectToActionResult.ActionName);
            Assert.Equal("HrScheduler", redirectToActionResult.ControllerName);

            // Verify that the UpdateSchedule method was called with the correct schedule
            _scheduleService.Verify(x => x.UpdateSchedule(It.IsAny<Schedule>()), Times.Once);
        }

        [Fact]
        public void UpdateSchedule_InvalidSchedule_ThrowsException()
        {
            // Arrange
            var schedule = new Schedule
            {
                ScheduleId = 1,
                InterviewerId = 1,
                // Missing required JobId to make the schedule invalid
                StartTime = "10:00 AM",
                EndTime = "12:00 PM",
                Date = "2023-07-15",
                Instruction = "Some instructions"
            };

            // Setup the service to throw an exception when UpdateSchedule is called
            _scheduleService.Setup(x => x.UpdateSchedule(schedule)).Throws(new Exception());

            // Act and Assert
            Assert.Throws<Exception>(() => _controller.UpdateSchedule(schedule));

            // Verify that the UpdateSchedule method was called once with the provided schedule
            _scheduleService.Verify(x => x.UpdateSchedule(schedule), Times.Once);
        }

        [Fact]
        public void DeleteSchedule_ValidId_ReturnsRedirectToAction()
        {
            // Arrange
            int scheduleId = 1;

            // Act
            var result = _controller.DeleteSchedule(scheduleId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("HrScheduler", redirectToActionResult.ControllerName);
            Assert.Equal("home", redirectToActionResult.ActionName);

            // Verify that the DeleteSchedule method of the service was called once with the provided scheduleId
            _scheduleService.Verify(x => x.DeleteSchedule(scheduleId), Times.Once);
        }

        [Fact]
        public void ApplicantList_ReturnsViewResult()
        {
            // Act
            var result = _controller.ApplicantList();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }
    }
}
