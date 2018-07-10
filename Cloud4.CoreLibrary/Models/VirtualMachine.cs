using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualMachine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string VirtualMachineProfile { get; set; }
        public Guid VirtualDatacenterId { get; set; }
        public string State { get; set; }
        public string OsType { get; set; }
        public Guid? AvailabilitySetId { get; set; }
        public Guid ImageId { get; set; }
        public VirtualDisk OsDisk { get; set; }
        public List<VirtualDisk> DataDisks { get; set; }
        public List<VirtualNetworkAdapter> NetworkInterfaces { get; set; }
        public VirtualMachineSetting SetupSetting { get; set; }

        public string RemoteAccessAddress { get; set; }

        public bool IsRemoteAccessEnabled { get; set; }

        public bool IsInternetAccessEnabled { get; set; }
        public List<string> ConfigurableVirtualNetworkAdapterProfiles { get; set; }

        public int MaxVirtualNetworkAdapterCount { get; set; }
    }


}
