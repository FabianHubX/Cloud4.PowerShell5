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
    [Cmdlet(VerbsCommon.Get, "Cloud4Job")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class GetJobs : BaseGetCmdLet<CoreLibrary.Models.Job, JobService>
    {     

        [Parameter(
           Mandatory = false,
           Position = 0,
           ValueFromPipeline = true,
           HelpMessage = "Filter by Job Id",
           ValueFromPipelineByPropertyName = true)]
      
        public Guid Id { get; set; }

        private JobService JobService { get; set; }


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
