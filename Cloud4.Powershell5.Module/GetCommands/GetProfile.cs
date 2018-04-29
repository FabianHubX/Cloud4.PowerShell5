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
    [Cmdlet(VerbsCommon.Get, "Cloud4Profile")]
    [OutputType(typeof(string))]
    public class GetProfile : BaseGetCmdLet<VirtualLoadBalancer, VirtualLoadBalancerService>
    {




        [Parameter(
           Mandatory = true,
           Position = 0,
           ValueFromPipeline = true,
            HelpMessage = "Profile Type",
           ValueFromPipelineByPropertyName = true)]

        public ProfileType Type { get; set; }

        private VirtualDiskProfileService DiskProfileService { get; set; }
        private VirtualMachineProfileService MachineProfileService { get; set; }
        private VirtualNetworkAdapterProfileService NetworkAdapterProfileService { get; set; }


        protected override void ProcessRecord()
        {


            try
            {
                switch (Type)
                {
                    case ProfileType.VirtualDisk:
                        DiskProfileService = new VirtualDiskProfileService(Connection);

                        Task<List<VirtualDiskProfile>> callTask1 = Task.Run(() => DiskProfileService.AllAsync());

                        callTask1.Wait();
                        var diskprofiles = callTask1.Result;
                        diskprofiles.ToList().ForEach(WriteObject);
                        break;


                    case ProfileType.VirtualMachine:
                        MachineProfileService = new VirtualMachineProfileService(Connection);

                        Task<List<VirtualMachineProfile>> callTask2 = Task.Run(() => MachineProfileService.AllAsync());

                        callTask2.Wait();
                        var vmprofiles = callTask2.Result;
                        vmprofiles.ToList().ForEach(WriteObject);
                        break;

                    case ProfileType.VirtualNetworkAdapter:
                        NetworkAdapterProfileService = new VirtualNetworkAdapterProfileService(Connection);

                        Task<List<VirtualNetworkAdapterProfile>> callTask3 = Task.Run(() => NetworkAdapterProfileService.AllAsync());

                        callTask3.Wait();
                        var adapterprofiles = callTask3.Result;
                        adapterprofiles.ToList().ForEach(WriteObject);
                        break;
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
