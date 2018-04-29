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
    public class GetVirtualNetAdapter : BaseGetCmdLet<VirtualNetworkAdapter, VirtualNetworkAdapterService>
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


        protected override void ProcessRecord()
        {

            if (Id == Guid.Empty)
            {
                if (VirtualMachineId == Guid.Empty)
                {

                    GetAll(Connection).ForEach(WriteObject);
                }
                else
                {

                    GetbyVmAll(VirtualMachineId, Connection).ForEach(WriteObject);
                }
            }
            else
            {

                WriteObject(GetOne(Id, Connection));



            }
        }

        protected override void EndProcessing()
        {

        }



        public static List<VirtualNetworkAdapter> GetbyVmAll(Guid vMId, Connection con)
        {
            try
            {
                List<VirtualNetworkAdapter> newlist = new List<VirtualNetworkAdapter>();

                VirtualMachineService service = new VirtualMachineService(con);



                Task<Cloud4.CoreLibrary.Models.VirtualMachine> callTaskVM = Task.Run(() => service.GetAsync(vMId));

                callTaskVM.Wait();
                var vm = callTaskVM.Result;

                if (vm != null)
                {
                    newlist.AddRange(vm.NetworkInterfaces);
                }

                return newlist;

            }
            catch (Exception e)
            {
                throw new RemoteException("An API Error has happen");
            }
        }

    }
}
