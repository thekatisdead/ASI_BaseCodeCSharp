using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Test.Controllers
{
    public class JobOpeningControllerTests
    {
        private readonly JobOpeningController _controller;
        private readonly Mock<IJobOpeningService> _mockJobOpeningService;
        public JobOpeningControllerTests()
        {
            _mockJobOpeningService = new Mock<IJobOpeningService>();
            _controller = new JobOpeningController(_mockJobOpeningService.Object);
        }
    }
}
