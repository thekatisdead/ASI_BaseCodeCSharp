using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class CharacterReference
    {
        public int Id { get; set; }
        public string? CandidateFirstName { get; set; }
        public string? CandidateLastName { get; set; }
        public string? Position { get; set; }
        public string? RelationshipDuration { get; set; }
        public string? Relationship { get; set; }
        public string? CharacterEthics { get; set; }
        public string? Qualifications { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? JobTitle { get; set; }
        public bool WorkedWithCandidate { get; set; }
        public string? ReasonToHire { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; } = System.Environment.UserName;
        public DateTime? UpdatedTime { get; set; }
        public string? UpdatedBy { get; set; } = System.Environment.UserName;
    }
}
