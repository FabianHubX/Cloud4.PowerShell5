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
    [Cmdlet(VerbsCommon.New, "Cloud4vNet")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualNet : BaseTenantNewCmdLet<VirtualNetwork, VirtualNetworkService, CreateVirtualNetwork>
    {
        private List<string> _dnsservers;

        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Name of the new virtual Network",
          ValueFromPipelineByPropertyName = true)]
      
        public string Name { get; set; }

        [Parameter(
        Mandatory = true,
        Position = 1,
        ValueFromPipeline = true,
            HelpMessage = "Address Space for the virtual Network",
        ValueFromPipelineByPropertyName = true)]
      
        public string AddressSpace { get; set; }

     
        [Parameter(
           Mandatory = true,
           Position = 2,
           ValueFromPipeline = true,
            HelpMessage = "Virtual Datacenter where the Network gets created",
           ValueFromPipelineByPropertyName = true)]
      
        public Guid VirtualDataCenterId { get; set; }

        [Parameter(
          Mandatory = true,
          Position = 3,
          ValueFromPipeline = true,
            HelpMessage = "New Subnet's for this Virtual Network",
          ValueFromPipelineByPropertyName = true)]
      
        public List<CreateVirtualSubNet> SubNet { get; set; }


        [Parameter(
  Mandatory = true,
  Position = 4,
  ValueFromPipeline = true,
    HelpMessage = "New Set of DNS Servers for this Virtual Network",
  ValueFromPipelineByPropertyName = true)]

        public List<string> DnsServers { get => _dnsservers; set => _dnsservers = value; }


        [Parameter(
     Mandatory = false,
     Position = 5,
     ValueFromPipeline = true,
      HelpMessage = "Wait Job Finished",
     ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }



        protected override void ProcessRecord()
        {

            var vnet = new CreateVirtualNetwork
            {
                Name = Name,
                VirtualDatacenterId = VirtualDataCenterId,
                AddressSpace = new[] { AddressSpace },
                DnsServers = _dnsservers.ToArray(),
                SubNets = SubNet
            };

            var job = Create(Connection, vnet);

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
