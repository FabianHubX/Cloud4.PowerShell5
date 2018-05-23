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
    [Cmdlet(VerbsCommon.New, "Cloud4vGW")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualGateway : BaseNewCmdLet<VirtualGateway, VirtualGatewayService, CreateVirtualGateway>
    {
       

        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Name of the new Virtual Gateway",
          ValueFromPipelineByPropertyName = true)]
      
        public string Name { get; set; }


     
        [Parameter(
           Mandatory = true,
           Position = 1,
           ValueFromPipeline = true,
            HelpMessage = "Virtual Datacenter where the Virtual Gateway gets created",
           ValueFromPipelineByPropertyName = true)]
      
        public Guid VirtualDataCenterId { get; set; }


        [Parameter(
        Mandatory = true,
        Position = 2,
        ValueFromPipeline = true,
         HelpMessage = "Virtual SubNet where the LVirtual Gatway gets assigned to",
        ValueFromPipelineByPropertyName = true)]

        public Guid VirtualSubNetId { get; set; }




        [Parameter(
     Mandatory = false,
     Position = 5,
     ValueFromPipeline = true,
      HelpMessage = "Wait Job Finished",
     ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }



        protected override void ProcessRecord()
        {


            var vgw = new CreateVirtualGateway
            {
                 Name = Name,
                  VirtualDatacenterId = VirtualDataCenterId,
                  VirtualSubnetId = VirtualSubNetId
            };

            var job = Create(Connection, vgw);


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
