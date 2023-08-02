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
        public Schedule GetById(int id);
        public void UpdateSchedule(Schedule schedule);
        public void DeleteSchedule(Schedule schedule);
        public int GetMostRecentSchedId();
    }
}
