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
    [Cmdlet(VerbsCommon.Get, "Cloud4AvailabilitySet")]
    [OutputType(typeof(AvailabilitySet))]
    public class GetAvailabilitySet : BaseTenantGetCmdLet<AvailabilitySet, AvailabilitySetService>
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
     HelpMessage = "Filter by AvailabilitySet Name",
    ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }

        [Parameter(
 Mandatory = false,
 Position = 1,
 ValueFromPipeline = true,
  HelpMessage = "Filter by Virtual DataCenter ID",
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

                GetAll(Connection).Where(x => x.VirtualDatacenterId  == VirtualDatacenterId).ToList().ForEach(WriteObject);

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
