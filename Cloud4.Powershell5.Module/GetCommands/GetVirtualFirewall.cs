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

        public Guid? Id { get; set; }


        [Parameter(
    Mandatory = false,
    Position = 1,
    ValueFromPipeline = true,
     HelpMessage = "Filter by Virtual Firewall Name",
    ValueFromPipelineByPropertyName = true)]

        public string Name { get; set;}
        [Parameter(
 Mandatory = false,
 Position = 2,
 ValueFromPipeline = true,
  HelpMessage = "Filter by Virtual DataCenter ID",
 ValueFromPipelineByPropertyName = true)]

        public Guid? VirtualDatacenterId { get; set; }


        [Parameter(
Mandatory = false,
Position = 2,
ValueFromPipeline = true,
HelpMessage = "Filter by Virtual SubNet ID",
ValueFromPipelineByPropertyName = true)]

        public Guid? VirtualSubNetId { get; set; }

        [Parameter(
Mandatory = false,
Position = 2,
ValueFromPipeline = true,
HelpMessage = "Filter by Virtual NetAdapter ID",
ValueFromPipelineByPropertyName = true)]

        public Guid? VirtualNetAdapterId { get; set; }

        protected override void ProcessRecord()
        {
            if (!string.IsNullOrEmpty(Name))
            {

                var pattern = new WildcardPattern(Name);
                GetAll(Connection).Where(x => pattern.IsMatch(x.Name)).ToList().ForEach(WriteObject);

            }
            else if (Id.HasValue)
            {
               WriteObject(GetOne(Id.Value, Connection));

            }          
            else if (VirtualDatacenterId.HasValue)
            {

                GetAll(Connection).Where(x => x.VirtualDatacenterId == VirtualDatacenterId.Value).ToList().ForEach(WriteObject);

            }
            else
            {

                GetAll(Connection).ForEach(WriteObject);
            }
        }
        
        
    }
}
