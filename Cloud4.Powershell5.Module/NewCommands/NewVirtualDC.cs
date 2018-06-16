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
    [Cmdlet(VerbsCommon.New, "Cloud4vDC")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualDC : BaseTenantNewCmdLet<VirtualDatacenter,VirtualDataCenterService, VirtualDatacenter>
    {
        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Name of the new Datacenter",
          ValueFromPipelineByPropertyName = true)]
       
        public string Name { get; set; }

       

        [Parameter(
           Mandatory = true,
           Position = 1,
           ValueFromPipeline = true,
            HelpMessage = "Region where the virtual Datacenter get created",
           ValueFromPipelineByPropertyName = true)]
      
        public Guid RegionId { get; set; }

        [Parameter(
      Mandatory = false,
      Position = 2,
      ValueFromPipeline = true,
       HelpMessage = "Wait Job Finished",
      ValueFromPipelineByPropertyName = true)]

        public SwitchParameter Wait { get; set; }


        protected override void ProcessRecord()
        {


            var newvdc = new VirtualDatacenter { Name = Name, RegionId = RegionId, TenantId = Connection.TenantId };

            var job = Create(Connection, newvdc);


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
