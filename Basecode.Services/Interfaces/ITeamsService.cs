using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface ITeamsService
    {
        /// <summary>
        /// Generates a Teams Link and Send it to the attendee email
        /// </summary>
        public string GenerateTeamsMeetingLink(string meetingSubject, int startHour, int endHour, string attendeeEmails);
    }
}
