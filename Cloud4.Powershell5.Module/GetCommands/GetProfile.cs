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
    public class GetProfile : BaseGetCmdLet<VirtualDiskProfile, VirtualDiskProfileService>
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

                        Task<Result<List<VirtualDiskProfile>>> callTask1 = Task.Run(() => DiskProfileService.AllAsync());

                        callTask1.Wait();
                        var result1 = callTask1.Result;

                        if (result1.Object != default(List<VirtualDiskProfile>))
                        {
                            result1.Object.ToList().ForEach(WriteObject);
                        }
                        else if (result1.Error != null)
                        {
                            throw new RemoteException("Conflict Error: " + result1.Error.ErrorType + "\r\n" + result1.Error.FaultyValues);
                        }
                        else
                        {
                            throw new RemoteException("API returns: " + result1.Code.ToString());
                        }
                        break;
                        
                    case ProfileType.VirtualMachine:

                        MachineProfileService = new VirtualMachineProfileService(Connection);

                        Task<Result<List<VirtualMachineProfile>>> callTask2 = Task.Run(() => MachineProfileService.AllAsync());

                        callTask2.Wait();
                        var result2 = callTask2.Result;

                        if (result2.Object != default(List<VirtualMachineProfile>))
                        {
                            result2.Object.ToList().ForEach(WriteObject);
                        }
                        else if (result2.Error != null)
                        {
                            throw new RemoteException("Conflict Error: " + result2.Error.ErrorType + "\r\n" + result2.Error.FaultyValues);
                        }
                        else
                        {
                            throw new RemoteException("API returns: " + result2.Code.ToString());
                        }
                        break;

                    case ProfileType.VirtualNetworkAdapter:
                        NetworkAdapterProfileService = new VirtualNetworkAdapterProfileService(Connection);

                        Task<Result<List<VirtualNetworkAdapterProfile>>> callTask3 = Task.Run(() => NetworkAdapterProfileService.AllAsync());

                        callTask3.Wait();
                        var result3 = callTask3.Result;

                        if (result3.Object != default(List<VirtualNetworkAdapterProfile>))
                        {
                            result3.Object.ToList().ForEach(WriteObject);
                        }
                        else if (result3.Error != null)
                        {
                            throw new RemoteException("Conflict Error: " + result3.Error.ErrorType + "\r\n" + result3.Error.FaultyValues);
                        }
                        else
                        {
                            throw new RemoteException("API returns: " + result3.Code.ToString());
                        }
                        break;
                }



            }
            catch (Exception e)
            {
                throw new RemoteException("An API Error has happen");
            }
        }

        
        
    }
}
