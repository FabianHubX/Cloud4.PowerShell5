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
    [Cmdlet(VerbsCommon.Get, "Cloud4Company")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Company))]
    public class GetCompany : BaseGetCmdLet<Company, CompanyService>
    {
        
        [Parameter(
           Mandatory = false,
           Position = 0,
           ValueFromPipeline = true,
            HelpMessage = "Filter by Current Company",
           ValueFromPipelineByPropertyName = true)]
        [PSDefaultValue(Value = true)]
        public bool? Current { get; set; }

        [Parameter(
          Mandatory = false,
          Position = 0,
          ValueFromPipeline = true,
           HelpMessage = "Filter by Company Id",
          ValueFromPipelineByPropertyName = true)]
        [PSDefaultValue(Value = true)]
        public Guid Id { get; set; }

        protected override void ProcessRecord()
        {

            if (Current.HasValue)
            {

                GetAll(Connection).ToList().ForEach(WriteObject);

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
