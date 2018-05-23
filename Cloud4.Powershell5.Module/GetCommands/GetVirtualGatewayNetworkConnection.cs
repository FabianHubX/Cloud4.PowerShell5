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
    [Cmdlet(VerbsCommon.Get, "Cloud4vGWNetConnection")]
    [OutputType(typeof(VirtualGatewayNetworkConnection))]
    public class GetVirtualGatewayNetworkConnection :  BaseGetVirtualGatewayCmdLet<VirtualGatewayNetworkConnection, VirtualGatewayNetworkConnectionService>
    {
       
      
        [Parameter(
           Mandatory = true,
           Position = 0,
           ValueFromPipeline = true,
            HelpMessage = "Filter by Id",
           ValueFromPipelineByPropertyName = true)]
      
        public Guid VirtualGatewayId { get; set; }

        [Parameter(
   Mandatory = false,
   Position = 1,
   ValueFromPipeline = true,
    HelpMessage = "Filter by Network Connection Id",
   ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }


        protected override void ProcessRecord()
        {

            if (Id == Guid.Empty)
            {
                GetAll(Connection, VirtualGatewayId).ForEach(WriteObject);
            }
            else
            {
                WriteObject(GetOne(Id, Connection, VirtualGatewayId));
            }


        }

        
        
    }
}
