using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud4.CoreLibrary.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }
        public string JobType { get; set; }
        public string ErrorMessage { get; set; }
        public string State { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }


    }
}
