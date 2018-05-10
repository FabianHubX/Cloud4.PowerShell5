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
    [Cmdlet(VerbsCommon.New, "Cloud4vLB")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualLoadBalancer : BaseNewCmdLet<VirtualLoadBalancer, VirtualLoadBalancerService, CreateVirtualLoadBalancer>
    {
       

        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Name of the new virtual Load Balancer",
          ValueFromPipelineByPropertyName = true)]
      
        public string Name { get; set; }

        [Parameter(
        Mandatory = false,
        Position = 1,
        ValueFromPipeline = true,
            HelpMessage = "BackEnd Addresspool for the virtual Load Balancer",
        ValueFromPipelineByPropertyName = true)]
      
        public List<CreateVirtualLoadBalancerBackEndPool> BackendAddressPool { get; set; }

     
        [Parameter(
           Mandatory = true,
           Position = 2,
           ValueFromPipeline = true,
            HelpMessage = "Virtual Datacenter where the Load Balancer gets created",
           ValueFromPipelineByPropertyName = true)]
      
        public Guid VirtualDataCenterId { get; set; }

        [Parameter(
        Mandatory = false,
        Position = 3,
        ValueFromPipeline = true,
            HelpMessage = "FrontEnd IP Configuration for the virtual Load Balancer",
        ValueFromPipelineByPropertyName = true)]

        public List<CreateVirtualLoadBalancerFrontEndIPConfigurations> FrontEndIPConfiguration { get; set; }




        [Parameter(
     Mandatory = false,
     Position = 5,
     ValueFromPipeline = true,
      HelpMessage = "Wait Job Finished",
     ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }



        protected override void ProcessRecord()
        {


            var vlb = new CreateVirtualLoadBalancer
            {
                BackendAddressPools = BackendAddressPool,
                FrontEndIPConfigurations = FrontEndIPConfiguration,
                Name = Name,
                Probes = null,
                VirtualDatacenterId = VirtualDataCenterId
            };

            var job = Create(Connection, vlb);


            if (Wait)
            {
                WriteObject(WaitJobFinished(job.Id,Connection));
            }
            else
            {
                WriteObject(job);
            }

        }

        
        
    }
}
