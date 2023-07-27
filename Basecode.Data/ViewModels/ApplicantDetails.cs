using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    public class ApplicantDetails
    {
        public int ApplicantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string EmploymentType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Time { get; set; } 
        public string? School { get; set; }
        public string? SchoolDepartment { get; set; }   
        public string? Achievements { get; set; }      
        public string? ReferenceOneFullName { get; set; }
        public string? RelationshipOne { get; set; } 
        public string? ContactInfoOne { get; set; } 
        public string? ReferenceTwoFullName { get; set; }   
        public string? RelationshipTwo { get; set; }
        public string? ContactInfoTwo { get; set; }
        public string? ReferenceThreeFullName { get; set; }
        public string? RelationshipThree { get; set; }
        public string? ContactInfoThree { get; set; }
        public byte[]? CurriculumVitae { get; set; }
    }
}
