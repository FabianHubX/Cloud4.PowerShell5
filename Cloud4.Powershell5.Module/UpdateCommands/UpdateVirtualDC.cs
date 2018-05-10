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
    [Cmdlet(VerbsData.Update, "Cloud4vDC")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class UpdateVirtualDC : BaseUpdateCmdLet<VirtualDatacenter,VirtualDataCenterService, VirtualDatacenter>
    {

        [Parameter(
Mandatory = true,
Position = 0,
ValueFromPipeline = true,
HelpMessage = "Filter by vDC Id",
ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

        [Parameter(
          Mandatory = true,
          Position = 1,
          ValueFromPipeline = true,
            HelpMessage = "Name of the Datacenter",
          ValueFromPipelineByPropertyName = true)]
       
        public string Name { get; set; }





        [Parameter(
      Mandatory = false,
      Position = 2,
      ValueFromPipeline = true,
       HelpMessage = "Wait Job Finished",
      ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }



        protected override void ProcessRecord()
        {
            

            var vdc = Get(Connection, Id);

            vdc.Name = Name;

            var job = Update(Connection, Id, vdc);

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
