using System.ComponentModel.DataAnnotations;
using System;
using Basecode.WebApp.Models;
using Basecode.Data.Repositories;


namespace Basecode.WebApp.Models
{
    public class ApplicationTrackingModel
    {
        public required int Id { get; set;}
        public required string FirstName { get; set;}
        public required string LastName { get; set;}
        public required string EmailAddress { get; set;}
        public required int JobApplied { get; set;}
        public required string Tracker { get; set;}
        public required string Grading { get; set;}
        public DateTime? CreatedTime { get; set;}
        public string? CreatedBy { get; set;}
        public DateTime? UpdatedTime { get; set;}
        public string? UpdatedBy { get;set;}

        public static ApplicationTrackingModel Find(int ApplicantId)
        {
            //var applicantData = HRAutomationSystem.Query("SELECT * FROM Applicant WHERE Id = @ApplicantId", new { ApplicantId });
            ApplicationTrackingModel model = new ApplicationTrackingModel
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
        }
    }
}
