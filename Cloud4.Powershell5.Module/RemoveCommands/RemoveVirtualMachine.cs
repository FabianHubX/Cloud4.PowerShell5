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
    [Cmdlet(VerbsCommon.Remove, "Cloud4VM")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class RemoveVirtualMachine : BaseTenantRemoveCmdLet<VirtualMachine, VirtualMachineService>
    {
        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
          ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

        [Parameter(
         Mandatory = false,
         Position = 1,
         ValueFromPipeline = true,
         HelpMessage = "Force deleting attached virtual Firewalls",
         ValueFromPipelineByPropertyName = true)]

        public SwitchParameter ForceDeleteFW { get; set; }

        [Parameter(
        Mandatory = false,
        Position = 2,
        ValueFromPipeline = true,
        HelpMessage = "Wait Job Finished",
        ValueFromPipelineByPropertyName = true)]

        public SwitchParameter Wait { get; set; }


        protected override void ProcessRecord()
        {
            if(ForceDeleteFW.IsPresent)
            {
                var vm = Get(Connection, Id);
                foreach (var nic in vm.NetworkInterfaces)
                {
                    if (nic.VirtualFirewallId.HasValue)
                    {
                        RemoveVirtualFirewall.Remove(nic.VirtualFirewallId.Value, Connection, true);

                    }
                }
            }

            WriteObject(Remove(Id, Connection, Wait));

        }

        
        

    }
}
