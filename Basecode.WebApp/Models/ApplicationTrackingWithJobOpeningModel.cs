using System.ComponentModel.DataAnnotations;
using System;
using Basecode.Data.Repositories;


namespace Basecode.Data.Models
{
    public class ApplicationTrackingWithJobModel
    {
        public ApplicationTracking ApplicationTracking { get; set; }
        public JobOpening JobOpening { get; set; }
    }
}
