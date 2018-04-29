using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualDisk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string VirtualDiskProfileName { get; set; }
        public Guid? VirtualMachineId { get; set; }

     
    }
}
