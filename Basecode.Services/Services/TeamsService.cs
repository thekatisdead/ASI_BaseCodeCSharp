using Basecode.Services.Interfaces;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class TeamsService : ITeamsService
    {
        /// <summary>
        /// Generates a Teams Meeting Link and send it to the attendee
        /// </summary>
        /// <param name="meetingSubject"></param>
        /// <param name="startHour"></param>
        /// <param name="endHour"></param>
        /// <param name="attendeeEmails"></param>
        /// <returns></returns>
        public string GenerateTeamsMeetingLink(string meetingSubject, int startHour, int endHour, string attendeeEmails)
        {
            try
            {
                // Get the current date
                DateTime currentDate = DateTime.Now.Date;

                // Calculate the start time and end time based on the provided hours
                DateTime startTime = currentDate.AddHours(startHour);
                DateTime endTime = currentDate.AddHours(endHour);

                // Encode the meeting subject to be included in the URL
                string encodedSubject = Uri.EscapeDataString(meetingSubject);

                // Format the start time and end time in ISO 8601 format (UTC)
                string startTimeIso8601 = startTime.ToUniversalTime().ToString("o");
                string endTimeIso8601 = endTime.ToUniversalTime().ToString("o");

                string encodedEmails = Uri.EscapeDataString(attendeeEmails.Replace(",", ";")); // Replace commas with semicolons to avoid interference with URL parameters


                // Construct the Microsoft Teams meeting URL
                string teamsLink = $"https://teams.microsoft.com/l/meeting/new?subject={encodedSubject}&startTime={startTimeIso8601}&endTime={endTimeIso8601}&email={encodedEmails}&attendees={encodedEmails}";
                //string teamsLink = $"https://teams.microsoft.com/l/meeting/new?subject={encodedSubject}&startTime={startTimeIso8601}&endTime={endTimeIso8601}";
                return teamsLink;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the process
                // You might want to log or throw the exception based on your requirements
                return string.Empty;
            }
        }
    }
}
