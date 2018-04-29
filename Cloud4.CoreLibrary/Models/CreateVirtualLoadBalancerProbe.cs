using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class CreateVirtualLoadBalancerProbe
    {
    
        public int Port { get; set; }   // 1-65535
        public string Protocol { get; set; }  // TCP,HTTP
        public int NumberOfProbes { get; set; }      // 31   (11...
        public int IntervalInSeconds { get; set; }       // 15   (5-...
        public string RequestPath { get; set; }       // bei TCP muss der leer sein
      
    }
}
