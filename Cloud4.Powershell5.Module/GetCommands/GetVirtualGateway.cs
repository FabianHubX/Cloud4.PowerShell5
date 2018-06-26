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
    [Cmdlet(VerbsCommon.Get, "Cloud4vGW")]
    [OutputType(typeof(VirtualGateway))]
    public class GetVirtualGateway : BaseTenantGetCmdLet<VirtualGateway, VirtualGatewayService>
    {
       
      
        [Parameter(
           Mandatory = false,
           Position = 0,
           ValueFromPipeline = true,
            HelpMessage = "Filter by Id",
           ValueFromPipelineByPropertyName = true)]
      
        public Guid Id { get; set; }



        [Parameter(
    Mandatory = false,
    Position = 1,
    ValueFromPipeline = true,
     HelpMessage = "Filter by Name",
    ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }

        [Parameter(
        Mandatory = false,
        Position = 2,
        ValueFromPipeline = true,
           HelpMessage = "Filter by Virtual Datacenter Id",
        ValueFromPipelineByPropertyName = true)]

        public Guid VirtualDatacenterId { get; set; }
   

        protected override void ProcessRecord()
        {
            if (!string.IsNullOrEmpty(Name))
            {

                var pattern = new WildcardPattern(Name);
                GetAll(Connection).Where(x => pattern.IsMatch(x.Name)).ToList().ForEach(WriteObject);

            }
            else if (VirtualDatacenterId != Guid.Empty)
            {

                GetAll(Connection).Where(x => x.VirtualDatacenterId == VirtualDatacenterId).ToList().ForEach(WriteObject);

            }
            else if (Id == Guid.Empty)
            {
                GetAll(Connection).ForEach(WriteObject);
            }
            else
            {
               WriteObject(GetOne(Id, Connection));
            }

        }

        public static List<VirtualGateway> GetbyvDCAll(Guid vDCId, Connection con)
        {
            return GetAll(con).Where(x => x.VirtualDatacenterId == vDCId).ToList();
        }

    }
}
