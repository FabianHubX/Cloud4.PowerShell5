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
    [Cmdlet(VerbsCommon.Add, "Cloud4vNetAdapter")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class AddVirtualNetAdapter : BaseCmdLet
    {

        private string _nICProfile;


        [Parameter(
           Mandatory = true,
           Position = 0,
           ValueFromPipeline = true,
            HelpMessage = "Virtual Maschine where the Virtual NetAdapter gets assigned too",
           ValueFromPipelineByPropertyName = true)]

        public Guid VirtualMachineId { get; set; }

        [Parameter(
         Mandatory = true,
         Position = 1,
         ValueFromPipeline = true,
          HelpMessage = "Virtual SubNet where the Virtual NetAdapter gets assigned too",
         ValueFromPipelineByPropertyName = true)]

        public Guid VirtualSubNetId { get; set; }

        [Parameter(
           Mandatory = true,
           Position = 2,
           ValueFromPipeline = true,
            HelpMessage = "Your Virtual Network Adapter Profile",
           ValueFromPipelineByPropertyName = true)]

        public string NicProfile { get => _nICProfile; set => _nICProfile = value; }



        public bool Wait { get; set; }

        private VirtualNetworkAdapterService service { get; set; }

        private VirtualSubNetService subnetService { get; set; }



        protected override void ProcessRecord()
        {

            service = new VirtualNetworkAdapterService(Connection);
     

            subnetService = new VirtualSubNetService(Connection);



            Task<CoreLibrary.Models.VirtualSubNet> callTasksubNet = Task.Run(() => subnetService.GetAsync(VirtualSubNetId));

            callTasksubNet.Wait();
            var subnet = callTasksubNet.Result;



            var virtualnic = new CreateVirtualNetworkAdapter
            {
                IpAddress = subnet.NextFreeIpAddress,
                IpAllocationMethod = 0,
                SubNetId = VirtualSubNetId,
                VirtualNetworkAdapterProfileName = _nICProfile
            };





            Task<CoreLibrary.Models.Job> callTask = Task.Run(() => service.CreateAsync(virtualnic));

            callTask.Wait();
            var job = callTask.Result;
       

            if (Wait)
            {
                JobService jobService = new JobService(Connection);

                WaitJobFinished(job.Id);

                Task<List<VirtualNetworkAdapter>> callTasklist = Task.Run(() => service.AllAsync());

                callTasklist.Wait();
                var virtualDatacenters = callTasklist.Result;

                WriteObject(virtualDatacenters.FirstOrDefault(x => x.Id == job.ResourceId));
            }
            else
            {

                WriteObject(job);
            }

        }

        protected override void EndProcessing()
        {

        }
    }
}
