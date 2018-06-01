using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class CreateVirtualDisk
    {

        public Guid virtualMachineId { get; set; }
        public string Name { get; set; }
        public string VirtualDiskProfileName { get; set; }
     
     
    }
}
