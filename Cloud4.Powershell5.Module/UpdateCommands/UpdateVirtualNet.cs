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
    [Cmdlet(VerbsData.Update, "Cloud4vNet")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class UpdateVirtualNetwork : BaseTenantUpdateCmdLet< VirtualNetwork, VirtualNetworkService, VirtualNetwork>
    {
        [Parameter(
     Mandatory = true,
     Position = 0,
     ValueFromPipeline = true,
      HelpMessage = "Filter by vNet Id",
     ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

        [Parameter(
     Mandatory = true,
     Position = 1,
     ValueFromPipeline = true,
       HelpMessage = "Name of the virtual Network",
     ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }


        [Parameter(
  Mandatory = true,
  Position = 4,
  ValueFromPipeline = true,
    HelpMessage = "New Set of DNS Servers for this Virtual Network",
  ValueFromPipelineByPropertyName = true)]

        public List<string> DnsServers { get; set; }


        [Parameter(
      Mandatory = false,
      Position = 2,
      ValueFromPipeline = true,
       HelpMessage = "Wait Job Finished",
      ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }

        private VirtualNetworkService virtualNetworkService { get; set; }



        protected override void ProcessRecord()
        {

            var vnet = Get(Connection, Id);



            if (!string.IsNullOrEmpty(Name))
            {
                vnet.Name = Name;
            }
            if (DnsServers != null)
            {
                vnet.DnsServers = DnsServers.ToArray();
            }

            var job = Update(Connection, Id, vnet);

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
