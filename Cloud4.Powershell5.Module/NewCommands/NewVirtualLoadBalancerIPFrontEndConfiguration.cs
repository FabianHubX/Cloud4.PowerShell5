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
    [Cmdlet(VerbsCommon.New, "Cloud4vLBFrontEndIPConfigurations")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualLoadBalancerFrontEndIPConfiguration: BaseCmdLet
    {


        [Parameter(
         Mandatory = true,
         Position = 0,
         ValueFromPipeline = true,
           HelpMessage = "Id of the Virtual Load Balancer",
         ValueFromPipelineByPropertyName = true)]

        public Guid VirtualLoadBalancerId { get; set; }

        [Parameter(
         Mandatory = false,
         Position = 1,
         ValueFromPipeline = true,
           HelpMessage = "Internal IP Adress",
         ValueFromPipelineByPropertyName = true)]

        public string InternalIpAddress { get; set; }

  

        [Parameter(
       Mandatory = false,
       Position = 2,
       ValueFromPipeline = true,
         HelpMessage = "Assign a Public IP Address",
       ValueFromPipelineByPropertyName = true)]

        public bool AssignPublicIP { get; set; }

        [Parameter(
       Mandatory = true,
       Position = 3,
       ValueFromPipeline = true,
         HelpMessage = "Virtual Subnet Id",
       ValueFromPipelineByPropertyName = true)]

        public Guid VirtualSubnetId { get; set; }

        [Parameter(
         Mandatory = false,
         Position = 4,
         ValueFromPipeline = true,
          HelpMessage = "Wait Job Finished",
         ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }



        protected override void ProcessRecord()
        {

            VirtualLoadBalancerFrontendIpConfigurationsService service = new VirtualLoadBalancerFrontendIpConfigurationsService(Connection, VirtualLoadBalancerId);

            VirtualSubNetService subnetService = new VirtualSubNetService(Connection);

            Task<CoreLibrary.Models.VirtualSubNet> callTasksubNet = Task.Run(() => subnetService.GetAsync(VirtualSubnetId));

            callTasksubNet.Wait();
            var subnet = callTasksubNet.Result;

            if (string.IsNullOrEmpty(InternalIpAddress))
            {
                InternalIpAddress = subnet.NextFreeIpAddress;
            }


            try
            {
                var vlb = new CreateVirtualLoadBalancerFrontEndIPConfigurations
                {
                     InternalIpAddress= InternalIpAddress,
                      AssignPublicIp =  AssignPublicIP,
                       VirtualSubnetId = VirtualSubnetId

                };
               

                Task<CoreLibrary.Models.Job> callTask = Task.Run(() => service.CreateAsync(vlb));

                callTask.Wait();
                var job = callTask.Result;
                if (Wait)
                {
                    WaitJobFinished(job.Id);
                    Task<List<VirtualLoadBalancerFrontEndIPConfigurations>> callTasklist = Task.Run(() => service.AllAsync());

                    callTasklist.Wait();
                    var virtualnetworks = callTasklist.Result;

                    WriteObject(virtualnetworks.FirstOrDefault(x => x.Id == job.ResourceId));
                }
                else
                {
                    WriteObject(job);
                }



            }
            catch (Exception e)
            {
                throw new RemoteException("An API Error has happen");
            }

        }

        protected override void EndProcessing()
        {

        }
    }
}
