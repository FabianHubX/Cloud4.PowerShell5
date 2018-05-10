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
    [Cmdlet(VerbsCommon.New, "Cloud4vSubNet")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualSubNet : BaseNewCmdLet<VirtualSubNet, VirtualSubNetService, CreateVirtualSubNet>
    {
        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Name of the new Virtual SubNet",
          ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }

       
        [Parameter(
           Mandatory = true,
           Position = 1,
           ValueFromPipeline = true,
            HelpMessage = "Virtual Network where the virtual SubNet get created",
           ValueFromPipelineByPropertyName = true)]

        public Guid VirtualNetworkId { get; set; }


        [Parameter(
        Mandatory = true,
        Position = 2,
        ValueFromPipeline = true,
            HelpMessage = "Address Space for the virtual Network",
        ValueFromPipelineByPropertyName = true)]

        public string AddressSpace { get; set; }

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




            var newsubnet = new CreateVirtualSubNet { Name = Name, VirtualNetworkId = VirtualNetworkId, AddressPrefix = AddressSpace };

            var job = Create(Connection, newsubnet);

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
