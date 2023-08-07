using Basecode.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Basecode.Data
{
    public class BasecodeContext : IdentityDbContext<IdentityUser>
    {
        public BasecodeContext (DbContextOptions<BasecodeContext> options)
            : base(options)
        {}

        public void InsertNew(RefreshToken token)
        {
            var tokenModel = RefreshToken.SingleOrDefault(i => i.Username == token.Username);
            if (tokenModel != null)
            {
                RefreshToken.Remove(tokenModel);
                SaveChanges();
            }
            RefreshToken.Add(token);
            SaveChanges();
        }
        
        /// <summary>
        /// Represents the collection of applicants in the database.
        /// </summary>
        public virtual DbSet<Applicant> Applicant { get; set; }
        public virtual DbSet<JobOpening> JobOpening { get; set; }
        public virtual DbSet<CurrentHires> CurrentHires{ get; set; }
        public virtual DbSet<HrScheduler> HrScheduler { get; set; }
        public virtual DbSet<CharacterReference> CharacterReference { get; set; }
        public virtual DbSet<PublicApplicationForm> PublicApplicationForm { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<ApplicationTracking> ApplicationTracking { get; set; }
        public virtual DbSet<Interviewer> Interviewer { get; set; } 
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<ApplicantsSchedule> ApplicantsSchedule { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }

    }   
}