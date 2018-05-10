using Cloud4.CoreLibrary.Models;
using Cloud4.CoreLibrary.Services;
using Cloud4.Powershell5.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module
{
    [Cmdlet(VerbsCommon.Get, "Cloud4vLBFrontEndIPConfigurations")]
    [OutputType(typeof(VirtualLoadBalancerFrontEndIPConfigurations))]
    public class GetVirtualFrontEndIPConfigurations : BaseGetLoadBalancerCmdLet<VirtualLoadBalancerFrontEndIPConfigurations, VirtualLoadBalancerFrontendIpConfigurationsService>
    {
       
      
        [Parameter(
           Mandatory = true,
           Position = 0,
           ValueFromPipeline = true,
            HelpMessage = "Filter by vLoadBalancer Id",
           ValueFromPipelineByPropertyName = true)]
      
        public Guid VirtualLoadBalancerId { get; set; }

        [Parameter(
   Mandatory = false,
   Position = 1,
   ValueFromPipeline = true,
    HelpMessage = "Filter by FrontEnd Configuration Id",
   ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }


        protected override void ProcessRecord()
        {
            if (Id == Guid.Empty)
            {
                GetAll(Connection, VirtualLoadBalancerId).ForEach(WriteObject);
            }
            else
            {
                WriteObject(GetOne(Id, Connection, VirtualLoadBalancerId));
            }


        }

        
        
    }
}
