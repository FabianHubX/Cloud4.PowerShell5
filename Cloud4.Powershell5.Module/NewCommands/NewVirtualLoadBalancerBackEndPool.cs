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
    [Cmdlet(VerbsCommon.New, "Cloud4vLBBackEndPool")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualLoadBalancerBackEndPool : BaseNewLoadBalancerCmdLet<VirtualLoadBalancerBackEndPool, VirtualLoadBalancerBackEndPoolService, CreateVirtualLoadBalancerBackEndPool>
    {
      


        [Parameter(
      Mandatory = true,
      Position = 0,
      ValueFromPipeline = true,
          HelpMessage = "Virtual Machine Ids",
      ValueFromPipelineByPropertyName = true)]
        public List<Guid> VirtualMachineIds { get; set; }


        [Parameter(
         Mandatory = true,
         Position = 1,
         ValueFromPipeline = true,
           HelpMessage = "Id of the Virtual Load Balancer",
         ValueFromPipelineByPropertyName = true)]

        public Guid VirtualLoadBalancerId { get; set; }

        [Parameter(
         Mandatory = false,
         Position = 2,
         ValueFromPipeline = true,
          HelpMessage = "Wait Job Finished",
         ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }



        protected override void ProcessRecord()
        {


            var vlb = new CreateVirtualLoadBalancerBackEndPool
            {
                VirtualMachines = VirtualMachineIds

            };

            var job = Create(Connection, vlb, VirtualLoadBalancerId);

            if (Wait)
            {
                WriteObject(WaitJobFinished(job.Id, Connection, VirtualLoadBalancerId));

            }
            else
            {
                WriteObject(job);
            }


        }

        
        
    }
}
