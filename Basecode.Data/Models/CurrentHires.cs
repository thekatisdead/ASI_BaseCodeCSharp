using System.ComponentModel.DataAnnotations;
using System;
using Basecode.Data.Repositories;


namespace Basecode.Data.Models
{
    public class CurrentHires
    {
        public int Id { get; set;}
        public int ApplicantID { get; set;}
        public int JobID { get; set;}
        public DateTime? CreatedTime { get; set;}
        public string? CreatedBy { get; set;}
        public DateTime? UpdatedTime { get; set;}
        public string? UpdatedBy { get;set;}

        /*
        public static ApplicationTracking Find(int ApplicantId)
        {
            var applicantData = HRAutomationSystem.Query("SELECT * FROM Applicant WHERE Id = @ApplicantId", new { ApplicantId });
            ApplicationTracking model = new ApplicationTracking
            {
                Id = 0,
                FirstName = "Aima",
                LastName = "Gudgurl",
                EmailAddress = "",
                JobApplied = 0,
                Tracker = "",
                Grading = "Ongoing",
            };

            return model;
        }*/
    }
}
