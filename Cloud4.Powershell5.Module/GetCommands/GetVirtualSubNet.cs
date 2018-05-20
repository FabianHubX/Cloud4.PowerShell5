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
    [Cmdlet(VerbsCommon.Get, "Cloud4vSubNet")]
    [OutputType(typeof(VirtualSubNet))]
    public class GetVirtualSubNet : BaseGetCmdLet<VirtualSubNet, VirtualSubNetService>
    {
       
     
        [Parameter(
         Mandatory = false,
         Position = 0,
         ValueFromPipeline = true,
            HelpMessage = "Filter by Virtual SubNet Id",
         ValueFromPipelineByPropertyName = true)]
      
        public Guid Id { get; set; }

        [Parameter(
        Mandatory = false,
        Position = 1,
        ValueFromPipeline = true,
           HelpMessage = "Filter by Virtual Net Id",
        ValueFromPipelineByPropertyName = true)]

        public Guid VirtualNetworkId { get; set; }

        [Parameter(
        Mandatory = false,
        Position = 2,
        ValueFromPipeline = true,
         HelpMessage = "Filter by Name",
        ValueFromPipelineByPropertyName = true)]

        public string FilterByName { get; set; }

        protected override void ProcessRecord()
        {
            if (!string.IsNullOrEmpty(FilterByName))
            {

                GetAll(Connection).Where(x => x.Name == FilterByName).ToList().ForEach(WriteObject);

            }
            else if (Id == Guid.Empty)
            {
                if (VirtualNetworkId == Guid.Empty)
                {
                    GetAll(Connection).ForEach(WriteObject);
                }
                else
                {
                    GetByvNetAll(VirtualNetworkId, Connection).ForEach(WriteObject);
                }
            }
            else
            {
                WriteObject(GetOne(Id, Connection));
            }
        }




        public static List<VirtualSubNet> GetByvNetAll(Guid vNetId, Connection con)
        {
            VirtualSubNetService service = new VirtualSubNetService(con);

            Task<Result<List<VirtualSubNet>>> callTask = Task.Run(() => service.GetByvNetAsync(vNetId));
        
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
