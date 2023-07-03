﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class JobOpening
    {
        public int Id { get; set; }
        public string? Position { get; set; }
        public string? JobType { get; set; }
        public decimal Salary { get; set; }
        public int Hours { get; set; }
        public string? Shift { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string? CreatedBy { get; set; } 
        public DateTime? UpdatedTime { get;set; }
        public string? UpdatedBy { get;set; }
    }
}
