using Cloud4.CoreLibrary.Models;
using Cloud4.CoreLibrary.Services;
using Cloud4.Powershell5.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module
{
    [Cmdlet(VerbsCommon.Remove, "Cloud4Company")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class RemoveCompany : BaseRemoveCmdLet<Company, CompanyService>
    {
        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
          ValueFromPipelineByPropertyName = true)]
      
        public Guid Id  { get; set; }

        
        


        protected override void ProcessRecord()
        {


            WriteObject(Remove(Id, Connection, false));




        }




    }
}
