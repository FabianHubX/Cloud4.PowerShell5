using Cloud4.CoreLibrary.Models;
using Cloud4.CoreLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module
{
    [Cmdlet(VerbsCommon.Get, "Cloud4vFirewall")]
    [OutputType(typeof(VirtualFirewall))]
    public class GetVirtualFirewall : BaseTenantGetCmdLet<VirtualFirewall, VirtualFirewallService>
    {
        [Parameter(
        Mandatory = false,
        Position = 0,
        ValueFromPipeline = true,
           HelpMessage = "Filter by Virtual Firewall Id",
        ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }


        [Parameter(
    Mandatory = false,
    Position = 1,
    ValueFromPipeline = true,
     HelpMessage = "Filter by Virtual Firewall Name",
    ValueFromPipelineByPropertyName = true)]

        public string FilterByName { get; set;}
        [Parameter(
 Mandatory = false,
 Position = 2,
 ValueFromPipeline = true,
  HelpMessage = "Filter by Virtual DataCenter ID",
 ValueFromPipelineByPropertyName = true)]

        public Guid VirtualDatacenterId { get; set; }


        protected override void ProcessRecord()
        {
            if (!string.IsNullOrEmpty(FilterByName))
            {

                GetAll(Connection).Where(x => x.Name == FilterByName).ToList().ForEach(WriteObject);

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
        
        
    }
}
