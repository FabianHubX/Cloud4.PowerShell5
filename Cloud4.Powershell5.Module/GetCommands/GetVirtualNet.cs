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
    [Cmdlet(VerbsCommon.Get, "Cloud4vNet")]
    [OutputType(typeof(VirtualNetwork))]
    public class GetVirtualNet :  BaseGetCmdLet<VirtualNetwork, VirtualNetworkService>
    {
       

        [Parameter(
         Mandatory = false,
         Position = 0,
         ValueFromPipeline = true,
            HelpMessage = "Filter by Virtual Network Id",
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

        
        

        public static List<VirtualNetwork> GetbyvDCAll(Guid vDCId, Connection con)
        {
            VirtualNetworkService service = new VirtualNetworkService(con);

            Task<Result<List<VirtualNetwork>>> callTask = Task.Run(() => service.GetByvDCAsync(vDCId));

            callTask.Wait();

            var result = callTask.Result;


            if (result.Object != null)
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
