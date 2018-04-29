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
    [Cmdlet(VerbsCommon.Get, "Cloud4Region")]
    [OutputType(typeof(Region))]
    public class GetRegion : BaseGetCmdLet<Region, RegionService>
    {

       
   
        [Parameter(
           Mandatory = false,
           Position = 0,
           ValueFromPipeline = true,
            HelpMessage = "Filter by Region Id",
           ValueFromPipelineByPropertyName = true)]
       
        public Guid Id { get; set; }

        private RegionService service;


        protected override void ProcessRecord()
        {
            if (Id == Guid.Empty)
            {
                GetAll(Connection).ForEach(WriteObject);
            }
            else
            {
                WriteObject(GetOne(Id, Connection));
            }
        }

        protected override void EndProcessing()
        {
           
        }
    }
}
