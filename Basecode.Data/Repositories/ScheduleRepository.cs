using AutoMapper.Configuration.Conventions;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class ScheduleRepository:BaseRepository,IScheduleRepository
    {
        private readonly BasecodeContext _context;
        public ScheduleRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }
        public void Add(Schedule schedule)
        {
            _context.Schedule.Add(schedule);
            _context.SaveChanges();
        }
        public IQueryable<Schedule> GetAll()
        {
            return this.GetDbSet<Schedule>();
        }
        public Schedule GetById(int id)
        {
            return _context.Schedule.Find(id);
        }
        public void UpdateSchedule(Schedule schedule)
        {
            _context.Schedule.Update(schedule);
            _context.SaveChanges();
        }
        public void DeleteSchedule(Schedule schedule)
        {
            _context.Schedule.Remove(schedule);
            _context.SaveChanges();
        }
    }
}
