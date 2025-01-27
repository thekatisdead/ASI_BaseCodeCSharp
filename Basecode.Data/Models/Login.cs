﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; } = System.Environment.UserName;
        public DateTime? UpdatedTime { get; set; }
        public string? UpdatedBy { get; set; } = System.Environment.UserName;
    }
}
