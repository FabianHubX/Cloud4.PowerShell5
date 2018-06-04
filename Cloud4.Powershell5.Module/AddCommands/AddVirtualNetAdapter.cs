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
    public class AddVirtualNetAdapter : BaseTenantAddCmdLet<VirtualNetworkAdapter, VirtualNetworkAdapterService>
    {

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

        public string NicProfile { get; set; }

        [Parameter(
      Mandatory = true,
      Position = 2,
      ValueFromPipeline = true,
       HelpMessage = "Name for the Virtual Network Adapter",
      ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }


        [Parameter(
  Mandatory = false,
  Position = 4,
  ValueFromPipeline = true,
    HelpMessage = "New Set of DNS Servers for this Virtual Network Adapter",
  ValueFromPipelineByPropertyName = true)]

        public string[] DnsServers { get; set; }

        public bool Wait { get; set; }

        private VirtualNetworkAdapterService service { get; set; }

        private VirtualSubNetService subnetService { get; set; }



        protected override void ProcessRecord()
        {

            service = new VirtualNetworkAdapterService(Connection);
     

            subnetService = new VirtualSubNetService(Connection);

            VirtualSubNet subnet = new VirtualSubNet();


            Task<Result<CoreLibrary.Models.VirtualSubNet>> callTasksubNet = Task.Run(() => subnetService.GetAsync(VirtualSubNetId));

            callTasksubNet.Wait();

            if (callTasksubNet.Result.Object != null)
            {
                subnet = callTasksubNet.Result.Object;
            }
            else if (callTasksubNet.Result.Error != null)
            {
                throw new RemoteException("Conflict Error: " + callTasksubNet.Result.Error.ErrorType + "\r\n" + callTasksubNet.Result.Error.FaultyValues);
            }
            else
            {
                throw new RemoteException("API returns: " + callTasksubNet.Result.Code.ToString());
            }





            var virtualnic = new CreateVirtualNetworkAdapter
            {
                VirtualMachineId = VirtualMachineId,
                IpAddress = subnet.NextFreeIpAddress,
                Name = Name,
                DnsServers = DnsServers,
                SubNetId = VirtualSubNetId,
                VirtualNetworkAdapterProfileName = NicProfile
            };





            Task<CoreLibrary.Models.Result> callTask = Task.Run(() => service.CreateAsync(virtualnic));

            callTask.Wait();
            var result = callTask.Result;
            CoreLibrary.Models.Job job;

            if (result.Job != null)
            {
                job = result.Job;
            }
            else if (result.Error != null)
            {
                throw new RemoteException("Conflict Error: " + result.Error.ErrorType + "\r\n" + result.Error.FaultyValues);
            }
            else
            {
                throw new RemoteException("API returns: " + result.Code.ToString());
            }


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
