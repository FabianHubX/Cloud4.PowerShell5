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
    [Cmdlet(VerbsCommon.Get, "Cloud4vDisk")]
    [OutputType(typeof(VirtualDisk))]
    public class GetVirtualDisk : BaseGetCmdLet<VirtualDisk, VirtualDiskService>
    {
       

        [Parameter(
         Mandatory = false,
         Position = 0,
         ValueFromPipeline = true,
            HelpMessage = "Filter by Virtual Disk Id",
         ValueFromPipelineByPropertyName = true)]
     
        public Guid Id { get; set; }

        [Parameter(
         Mandatory = true,
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

                    GetAll(Connection).ToList().ForEach(WriteObject);
                }
                else
                {

                    var vm = GetVirtualMachine.GetOne(VirtualMachineId, Connection);                  

                    if (vm != null)
                    {
                        WriteObject(vm.OsDisk);
                        foreach (var netinterface in vm.DataDisks)
                        {
                            WriteObject(netinterface);
                        }
                    }
                }
            }
            else
            {
                WriteObject(GetOne(Id, Connection));
            }

        }

        
        

    }
}
