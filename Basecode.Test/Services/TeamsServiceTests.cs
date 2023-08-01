using Basecode.Services.Services;

namespace Basecode.Test.Services
{
    public class TeamsServiceTests
    {
        private readonly TeamsService _teamsService;

        public TeamsServiceTests()
        {
            _teamsService = new TeamsService();
        }

        [Fact]
        public void GenerateTeamsMeetingLink_ValidInput_ReturnsMeetingLink()
        {
            // Arrange
            string meetingSubject = "Test Meeting";
            int startHour = 14;
            int endHour = 15;
            string attendeeEmails = "attendee1@example.com,attendee2@example.com";

            // Act
            string teamsLink = _teamsService.GenerateTeamsMeetingLink(meetingSubject, startHour, endHour, attendeeEmails);

            // Assert
            Assert.NotNull(teamsLink);
            Assert.Contains("teams.microsoft.com", teamsLink);
            Assert.Contains($"subject={Uri.EscapeDataString(meetingSubject)}", teamsLink);
            Assert.Contains($"startTime={DateTime.Now.Date.AddHours(startHour).ToUniversalTime().ToString("o")}", teamsLink);
            Assert.Contains($"endTime={DateTime.Now.Date.AddHours(endHour).ToUniversalTime().ToString("o")}", teamsLink);
            Assert.Contains($"email={Uri.EscapeDataString(attendeeEmails.Replace(",", ";"))}", teamsLink);
            Assert.Contains($"attendees={Uri.EscapeDataString(attendeeEmails.Replace(",", ";"))}", teamsLink);
        }

        [Fact]
        public void GenerateTeamsMeetingLink_NonEmptySubject_ReturnsLinkWithEncodedSubject()
        {
            // Arrange
            string meetingSubject = "Test Meeting with Space and & Special Characters";
            int startHour = 14;
            int endHour = 15;
            string attendeeEmails = "attendee@example.com";

            // Act
            string teamsLink = _teamsService.GenerateTeamsMeetingLink(meetingSubject, startHour, endHour, attendeeEmails);

            // Assert
            Assert.Contains($"subject={Uri.EscapeDataString(meetingSubject)}", teamsLink);
        }

        [Fact]
        public void GenerateTeamsMeetingLink_StartAndEndTimesInUTC_ReturnsLinkWithCorrectTimes()
        {
            // Arrange
            string meetingSubject = "Test Meeting";
            int startHour = 14;
            int endHour = 15;
            string attendeeEmails = "attendee@example.com";

            // Act
            string teamsLink = _teamsService.GenerateTeamsMeetingLink(meetingSubject, startHour, endHour, attendeeEmails);

            // Assert
            Assert.Contains($"startTime={DateTime.Now.Date.AddHours(startHour).ToUniversalTime().ToString("o")}", teamsLink);
            Assert.Contains($"endTime={DateTime.Now.Date.AddHours(endHour).ToUniversalTime().ToString("o")}", teamsLink);
        }
    }
}