using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualLoadBalancerProbe
    {
        public Guid Id { get; set; }
        public int Port { get; set; }   
        public string Protocol { get; set; }
        public int NumberOfProbes { get; set; }
        public int IntervalInSeconds { get; set; }       
        public string RequestPath { get; set; }       
      
    }
}
