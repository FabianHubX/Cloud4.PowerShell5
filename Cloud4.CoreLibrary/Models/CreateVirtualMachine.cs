using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class CreateVirtualMachine
    {
        public string Name { get; set; }
        public string VirtualMachineProfileName { get; set; }
        public Guid VirtualDatacenterId { get; set; }

        public Guid ImageId { get; set; }
        public CreateVirtualDisk OsDisk { get; set; }
   
        public List<CreateVirtualNetworkAdapter> NetworkInterfaces { get; set; }

        public VirtualMachineSetting SetupSetting { get; set; }
        
        public CreateAvailabilitySet AvailabilitySet { get; set; }

        public List<CreateVirtualDisk> DataDisks { get; set; }

        public bool EnableRemoteAccess { get; set; }
        public bool EnableInternetAccess { get; set; }
        public bool EnableInOutboundVNetTraffic { get; set; }

    }



}
