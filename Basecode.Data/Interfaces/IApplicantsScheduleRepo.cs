using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Interfaces
{
    public interface IApplicantsScheduleRepo
    {
        /// <summary>
        /// Adds a new ApplicantsSchedule to the database.
        /// </summary>
        /// <param name="schedule">The ApplicantsSchedule object representing the schedule to be added.</param>
        public void Add(ApplicantsSchedule schedule);

        // <summary>
        /// Retrieves all ApplicantsSchedule from the database.
        /// </summary>
        /// <returns>An IQueryable collection of ApplicantsSchedule entities representing all schedules in the database.</returns>
        public IQueryable<ApplicantsSchedule> GetAll();
    }
}
