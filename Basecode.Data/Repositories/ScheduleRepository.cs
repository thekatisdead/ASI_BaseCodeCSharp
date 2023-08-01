using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Basecode.Data.Repositories
{
    public class ScheduleRepository : BaseRepository, IScheduleRepository
    {
        private readonly BasecodeContext _context;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ScheduleRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public int Add(Schedule schedule)
        {
            try
            {
                _context.Schedule.Add(schedule);
                _context.SaveChanges();
                _logger.Info("Schedule with ID {scheduleId} added successfully.", schedule.ScheduleId);
                return schedule.ScheduleId;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while adding a new schedule: {errorMessage}", ex.Message);
                return -1;
            }
        }

        public IQueryable<Schedule> GetAll()
        {
            try
            {
                _logger.Info("Retrieving all schedules from the database.");
                return this.GetDbSet<Schedule>();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while retrieving all schedules: {errorMessage}", ex.Message);
                throw;
            }
        }

        public Schedule GetById(int id)
        {
            try
            {
                _logger.Info("Retrieving schedule by ID: {applicantId}", id);
                return _context.Schedule.Find(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while retrieving schedule by ID {scheduleId}: {errorMessage}", id, ex.Message);
                throw;
            }
        }

        public void UpdateSchedule(Schedule schedule)
        {
            try
            {
                _context.Schedule.Update(schedule);
                _context.SaveChanges();
                _logger.Info("Schedule with ID {scheduleId} updated successfully.", schedule.ScheduleId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating schedule with ID {scheduleId}: {errorMessage}", schedule.ScheduleId, ex.Message);
                throw;
            }
        }

        public void DeleteSchedule(Schedule schedule)
        {
            try
            {
                _context.Schedule.Remove(schedule);
                _context.SaveChanges();
                _logger.Info("Schedule with ID {scheduleId} deleted successfully.", schedule.ScheduleId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting schedule with ID {scheduleId}: {errorMessage}", schedule.ScheduleId, ex.Message);
                throw;
            }
        }
    }
}