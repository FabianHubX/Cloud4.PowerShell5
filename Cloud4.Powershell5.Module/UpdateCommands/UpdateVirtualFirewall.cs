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
    [Cmdlet(VerbsData.Update, "Cloud4vFirewall")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class UpdateVirtualFirewall : BaseTenantUpdateCmdLet<VirtualFirewall, VirtualFirewallService, Cloud4.CoreLibrary.Models.UpdateVirtualFirewall>
    {
        [Parameter(
     Mandatory = true,
     Position = 0,
     ValueFromPipeline = true,
      HelpMessage = "Filter by vFirewall Id",
     ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

        [Parameter(
     Mandatory = false,
     Position = 1,
     ValueFromPipeline = true,
       HelpMessage = "Name of the virtual Firewall",
     ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }


        [Parameter(
     Mandatory = false,
     Position = 2,
     ValueFromPipeline = true,
       HelpMessage = "New Ruleset of the virtual Firewall",
     ValueFromPipelineByPropertyName = true)]

        public List<VirtualFirewallRule> Rules { get; set; }


        [Parameter(
      Mandatory = false,
      Position = 3,
      ValueFromPipeline = true,
       HelpMessage = "Wait Job Finished",
      ValueFromPipelineByPropertyName = true)]

        public SwitchParameter Wait { get; set; }



        protected override void ProcessRecord()
        {

     
            var vfw = Get(Connection, Id);

            var newfirewall = new Cloud4.CoreLibrary.Models.UpdateVirtualFirewall { Name = vfw.Name, Rules = vfw.Rules };


            bool IsChanged = false;

            if (!string.IsNullOrEmpty(Name))
            {
                newfirewall.Name = Name;
                IsChanged = true;
            }

            if (Rules != null)
            {
                newfirewall.Rules = Rules;
                IsChanged = true;
            }

            if (IsChanged)
            {

                var job = Update(Connection, Id, newfirewall);

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
}
