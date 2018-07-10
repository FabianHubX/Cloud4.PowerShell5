using Cloud4.CoreLibrary.Models;
using Cloud4.CoreLibrary.Services;
using Cloud4.Powershell5.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module
{
    [Cmdlet(VerbsCommon.New, "Cloud4vNetAdapter")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualNetAdapter : BaseTenantNewCmdLet<VirtualNetworkAdapter, VirtualNetworkAdapterService, CreateVirtualNetworkAdapter>
    {

        [Parameter(
           Mandatory = true,
           Position = 0,
           ValueFromPipeline = true,
            HelpMessage = "Virtual Maschine where the Virtual NetAdapter gets assigned too",
           ValueFromPipelineByPropertyName = true)]

        public Guid VirtualMachineId { get; set; }

        [Parameter(
         Mandatory = true,
         Position = 1,
         ValueFromPipeline = true,
          HelpMessage = "Virtual SubNet where the Virtual NetAdapter gets assigned too",
         ValueFromPipelineByPropertyName = true)]

        public Guid VirtualSubNetId { get; set; }

        [Parameter(
           Mandatory = true,
           Position = 2,
           ValueFromPipeline = true,
            HelpMessage = "Your Virtual Network Adapter Profile",
           ValueFromPipelineByPropertyName = true)]

        public string NicProfile { get; set; }

        [Parameter(
      Mandatory = true,
      Position = 2,
      ValueFromPipeline = true,
       HelpMessage = "Name for the Virtual Network Adapter",
      ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }


        [Parameter(
  Mandatory = false,
  Position = 4,
  ValueFromPipeline = true,
    HelpMessage = "New Set of DNS Servers for this Virtual Network Adapter",
  ValueFromPipelineByPropertyName = true)]

        public string[] DnsServers { get; set; }

        public SwitchParameter Wait { get; set; }


        protected override void ProcessRecord()
        {

            var subnet = GetSpecial<VirtualSubNet, VirtualSubNetService>(Connection, VirtualSubNetId);



            var virtualnic = new CreateVirtualNetworkAdapter
            {
                VirtualMachineId = VirtualMachineId,
                IpAddress = subnet.NextFreeIpAddress,
                Name = Name,
                DnsServers = DnsServers,
                SubNetId = VirtualSubNetId,
                VirtualNetworkAdapterProfileName = NicProfile
            };




            var job = Create(Connection, virtualnic);


            if (Wait)
            {
                WriteObject(WaitJobFinished(job.Id, Connection));
            }
            else
            {
                WriteObject(job);
            }

        }

    }
}
