using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Interfaces
{
    public interface IScheduleRepository
    {
        public int Add(Schedule schedule);
        public IQueryable<Schedule> GetAll();

        /// <summary>
        /// Retrieves a Schedule from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the Schedule to retrieve.</param>
        /// <returns>The Schedule entity with the specified ID if found; otherwise, returns null.</returns>
        public Schedule GetById(int id);

        /// <summary>
        /// Updates an existing Schedule in the database.
        /// </summary>
        /// <param name="schedule">The updated Schedule object.</param>
        public void UpdateSchedule(Schedule schedule);

        /// <summary>
        /// Deletes an existing Schedule from the database.
        /// </summary>
        /// <param name="schedule">The Schedule object to be deleted.</param>
        public void DeleteSchedule(Schedule schedule);

        /// <summary>
        /// Retrieves the ID of the most recent Schedule from the database.
        /// </summary>
        /// <returns>The ID of the most recent Schedule if found; otherwise, returns 0.</returns>
        public int GetMostRecentSchedId();
    }
}
