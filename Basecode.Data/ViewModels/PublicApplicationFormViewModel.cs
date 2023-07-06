using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    public class PublicApplicationFormViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
        public string? Time { get; set; }
        public string? PositionType { get; set; }
        public string? EmploymentType { get; set; }
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
        public string? CurriculumVitae { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; } = System.Environment.UserName;
    }
}
