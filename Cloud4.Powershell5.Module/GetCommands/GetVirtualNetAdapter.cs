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
    [Cmdlet(VerbsCommon.Get, "Cloud4vNetAdapter")]
    [OutputType(typeof(VirtualNetworkAdapter))]
    public class GetVirtualNetAdapter : BaseTenantGetCmdLet<VirtualNetworkAdapter, VirtualNetworkAdapterService>
    {
       

        [Parameter(
         Mandatory = false,
         Position = 0,
         ValueFromPipeline = true,
            HelpMessage = "Filter by Virtual Network Adapter Id",
         ValueFromPipelineByPropertyName = true)]
     
        public Guid Id { get; set; }

        [Parameter(
         Mandatory = false,
         Position = 0,
         ValueFromPipeline = true,
            HelpMessage = "Filter by Virtual Maschine Id",
         ValueFromPipelineByPropertyName = true)]

        public Guid VirtualMachineId { get; set; }

        [Parameter(
 Mandatory = false,
 Position = 0,
 ValueFromPipeline = true,
    HelpMessage = "Filter by Virtual Maschine ",
 ValueFromPipelineByPropertyName = true)]

        public VirtualMachine VirtualMachine { get; set; }

        [Parameter(
        Mandatory = false,
        Position = 0,
        ValueFromPipeline = true,
           HelpMessage = "Filter by Virtual Maschine Name",
        ValueFromPipelineByPropertyName = true)]

        public String VirtualMachineName { get; set; }


        protected override void ProcessRecord()
        {
            if (!string.IsNullOrEmpty(VirtualMachineName))
            {
                GetbyVmNameAll(VirtualMachineName, Connection).ForEach(WriteObject);
            }
            else if (Id == Guid.Empty)
            {
                if (VirtualMachineId == Guid.Empty)
                {

                    GetAll(Connection).ForEach(WriteObject);
                }
                else
                {

                    GetbyVmIdAll(VirtualMachineId, Connection).ForEach(WriteObject);
                }
            }
            else
            {

               WriteObject(GetOne(Id, Connection));



            }
        }

        
        



        public static List<VirtualNetworkAdapter> GetbyVmIdAll(Guid vMId, Connection con)
        {

            List<VirtualNetworkAdapter> newlist = new List<VirtualNetworkAdapter>();

            VirtualMachineService service = new VirtualMachineService(con);



            Task<Result<Cloud4.CoreLibrary.Models.VirtualMachine>> callTask = Task.Run(() => service.GetAsync(vMId));

            callTask.Wait();
            var result = callTask.Result;


            if (result.Object != null)
            {
                newlist.AddRange(result.Object.NetworkInterfaces);

                return newlist;
            }
            else if (result.Error != null)
            {
                throw new RemoteException("Conflict Error: " + result.Error.ErrorType + "\r\n" + result.Error.FaultyValues);
            }
            else
            {
                throw new RemoteException("API returns: " + result.Code.ToString());
            }
            
            

        }

        public static List<VirtualNetworkAdapter> GetbyVmNameAll(string vmName, Connection con)
        {

            List<VirtualNetworkAdapter> newlist = new List<VirtualNetworkAdapter>();

            VirtualMachineService service = new VirtualMachineService(con);



            Task<Result<List<Cloud4.CoreLibrary.Models.VirtualMachine>>> callTask = Task.Run(() => service.AllAsync());

            callTask.Wait();
            var result = callTask.Result;


            if (result.Object != null)
            {
                var vm = result.Object.FirstOrDefault(x => x.Name == vmName);
                if (vm != null)
                {
                    newlist.AddRange(vm.NetworkInterfaces);
                }
                else
                {
                    throw new RemoteException("VM not found");
                }
                return newlist;
            }
            else if (result.Error != null)
            {
                throw new RemoteException("Conflict Error: " + result.Error.ErrorType + "\r\n" + result.Error.FaultyValues);
            }
            else
            {
                throw new RemoteException("API returns: " + result.Code.ToString());
            }



        }

        public static List<VirtualNetworkAdapter> GetbyvSubnetAll(Guid VirtualSubNetId, Connection con)
        {

            VirtualNetworkAdapterService service = new VirtualNetworkAdapterService(con);



            Task<Result<List<VirtualNetworkAdapter>>> callTask = Task.Run(() => service.GetByvSubNetAsync(VirtualSubNetId));

            callTask.Wait();
            var result = callTask.Result;


            if (result.Object != default(List<VirtualNetworkAdapter>))
            {
                return result.Object;
            }
            else if (result.Error != null)
            {
                throw new RemoteException("Conflict Error: " + result.Error.ErrorType + "\r\n" + result.Error.FaultyValues);
            }
            else
            {
                throw new RemoteException("API returns: " + result.Code.ToString());
            }

        }

    }
}
