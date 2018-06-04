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
    [Cmdlet(VerbsCommon.New, "Cloud4vFirewall")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualFirewall : BaseTenantNewCmdLet<VirtualFirewall, VirtualFirewallService, VirtualFirewall>
    {
        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Name of the new virtual Firewall",
          ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }




        [Parameter(
           Mandatory = true,
           Position = 1,
           ValueFromPipeline = true,
            HelpMessage = "Virtual Datacenter where the Firewall gets created",
           ValueFromPipelineByPropertyName = true)]

        public Guid VirtualDataCenterId { get; set; }

        [Parameter(
   Mandatory = true,
   Position = 1,
   ValueFromPipeline = true,
    HelpMessage = "Firewall Rule Set",
   ValueFromPipelineByPropertyName = true)]

        public List<VirtualFirewallRule> Rules { get; set; }

        [Parameter(
     Mandatory = false,
     Position = 4,
     ValueFromPipeline = true,
      HelpMessage = "Wait Job Finished",
     ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }




        protected override void ProcessRecord()
        {

            var newfw = new VirtualFirewall { VirtualDatacenterId = VirtualDataCenterId, Name = Name, Rules = Rules };

            var job = Create(Connection, newfw);

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
