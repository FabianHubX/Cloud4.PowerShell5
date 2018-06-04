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
    [Cmdlet(VerbsCommon.New, "Cloud4AvailabilitySet")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewAvailabilitySet : BaseTenantNewCmdLet<AvailabilitySet, AvailabilitySetService, CreateAvailabilitySet>
    {
        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Name of the new AvailabilitySet",
          ValueFromPipelineByPropertyName = true)]
      
        public string Name { get; set; }

      
     
        [Parameter(
           Mandatory = true,
           Position = 2,
           ValueFromPipeline = true,
            HelpMessage = "Virtual Datacenter where the AvailabilitySet gets created",
           ValueFromPipelineByPropertyName = true)]
      
        public Guid VirtualDataCenterId { get; set; }
              

        [Parameter(
     Mandatory = false,
     Position = 4,
     ValueFromPipeline = true,
      HelpMessage = "Wait Job Finished",
     ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }


        private AvailabilitySetService service { get; set; }



        protected override void ProcessRecord()
        {

            var newaailset = new CreateAvailabilitySet { Name = Name, VirtualDatacenterId = VirtualDataCenterId };

            var job = Create(Connection, newaailset);


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
