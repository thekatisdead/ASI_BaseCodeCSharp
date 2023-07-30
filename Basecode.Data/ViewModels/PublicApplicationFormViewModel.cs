using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    public class PublicApplicationFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? LastName { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Contact number must be 11 digits.")]
        public string? PhoneNumber { get; set; }

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? Time { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? PositionType { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? EmploymentType { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? School { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? SchoolDepartment { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? Achievements { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? ReferenceOneFullName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? RelationshipOne { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? ContactInfoOne { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int? AnsweredOne { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? ReferenceTwoFullName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? RelationshipTwo { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? ContactInfoTwo { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int? AnsweredTwo { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? ReferenceThreeFullName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? RelationshipThree { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string? ContactInfoThree { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int? ApplicationID { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int? AnsweredThree { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public byte[]? CurriculumVitae { get; set; }

        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; } = System.Environment.UserName;
    }
}
