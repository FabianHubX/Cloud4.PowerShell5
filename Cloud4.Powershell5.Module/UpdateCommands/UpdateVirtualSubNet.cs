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
    [Cmdlet(VerbsData.Update, "Cloud4vSubNet")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class UpdateVirtualSubNet : BaseUpdateCmdLet<VirtualSubNet, VirtualSubNetService, VirtualSubNet>
    {
        [Parameter(
     Mandatory = true,
     Position = 0,
     ValueFromPipeline = true,
      HelpMessage = "Filter by vSubNet Id",
     ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

        [Parameter(
     Mandatory = false,
     Position = 1,
     ValueFromPipeline = true,
       HelpMessage = "Name of the virtual SubNet",
     ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }


        [Parameter(
     Mandatory = false,
     Position = 2,
     ValueFromPipeline = true,
       HelpMessage = "Id of the virtual Firewall",
     ValueFromPipelineByPropertyName = true)]

        public Guid VirtualFirewallId { get; set; }


        [Parameter(
      Mandatory = false,
      Position = 3,
      ValueFromPipeline = true,
       HelpMessage = "Wait Job Finished",
      ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }

        private VirtualSubNetService service { get; set; }



        protected override void ProcessRecord()
        {

            var vsubnet = Get(Connection, Id);

            bool IsChanged = false;

            if (!string.IsNullOrEmpty(Name))
            {
                vsubnet.Name = Name;
                IsChanged = true;
            }

            if (VirtualFirewallId != Guid.Empty)
            {
                vsubnet.VirtualFirewallId = VirtualFirewallId;
                IsChanged = true;
            }

            if (IsChanged)
            {

                var job = Update(Connection, Id, vsubnet);

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
