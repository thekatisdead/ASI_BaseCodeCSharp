using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Basecode.Data.ViewModels
{
    public class CharacterReferenceViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Candidate's First Name is required.")]
        public string CandidateFirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Candidate's Last Name is required.")]
        public string CandidateLastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Position is required.")]
        public string Position { get; set; } = string.Empty;

        [Required(ErrorMessage = "Relationship Duration is required.")]
        public string RelationshipDuration { get; set; } = string.Empty;

        [Required(ErrorMessage = "Relationship is required.")]
        public string Relationship { get; set; } = string.Empty;

        [Required(ErrorMessage = "Character Ethics is required.")]
        public string CharacterEthics { get; set; } = string.Empty;

        [Required(ErrorMessage = "Qualifications is required.")]
        public string Qualifications { get; set; } = string.Empty;

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Job Title is required.")]
        public string JobTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Worked with Candidate field is required.")]
        public bool WorkedWithCandidate { get; set; } 

        [Required(ErrorMessage = "Reason to Hire is required.")]
        public string ReasonToHire { get; set; } = string.Empty;

        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; } = System.Environment.UserName;
    }
}
