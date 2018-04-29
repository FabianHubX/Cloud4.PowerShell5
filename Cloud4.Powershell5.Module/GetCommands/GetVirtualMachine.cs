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
    [Cmdlet(VerbsCommon.Get, "Cloud4VM")]
    [OutputType(typeof(VirtualMachine))]
    public class GetVirtualMachine :   BaseGetCmdLet<VirtualMachine, VirtualMachineService>
    {
        
      

        [Parameter(
           Mandatory = false,
           Position = 0,
           ValueFromPipeline = true,
            HelpMessage = "Filter by Virual Machine Id",
           ValueFromPipelineByPropertyName = true)]
       
        public Guid Id { get; set; }

        [Parameter(
        Mandatory = false,
        Position = 1,
        ValueFromPipeline = true,
           HelpMessage = "Filter by Virtual Datacenter Id",
        ValueFromPipelineByPropertyName = true)]

        public Guid VirtualDatacenterId { get; set; }

        protected override void ProcessRecord()
        {
            if (Id == Guid.Empty)
            {
                if (VirtualDatacenterId == Guid.Empty)
                {
                    GetAll(Connection).ForEach(WriteObject);
                }
                else
                {
                    GetbyvDCAll(VirtualDatacenterId, Connection).ForEach(WriteObject);
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


        public static List<VirtualMachine> GetByvSubNetAll(Guid vSubNetId, Connection con)
        {
            List<VirtualMachine> newList = new List<VirtualMachine>();

            var result = GetAll(con);

            if (result != null)
            {
              
                foreach (var vm in result)
                {
                    bool addtolist = false;
                    foreach (var netadapter in vm.NetworkInterfaces)
                    {
                        if (netadapter.SubNetId == vSubNetId)
                        {
                            addtolist = true;
                        }
                    }

                    if (addtolist)
                    {
                        newList.Add(vm);
                    }
                }

            }

            return newList;
        }

        public static List<VirtualMachine> GetbyvDCAll(Guid vDCId, Connection con)
        {
            try
            {
                VirtualMachineService service = new VirtualMachineService(con);

                Task<List<VirtualMachine>> callTask = Task.Run(() => service.GetByvDCAsync(vDCId));

                callTask.Wait();
                var job = callTask.Result;

                return job;

            }
            catch (Exception e)
            {
                throw new RemoteException("An API Error has happen");
            }
        }

    }
}
