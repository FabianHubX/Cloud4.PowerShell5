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
    [Cmdlet(VerbsCommon.Get, "Cloud4Contact")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Contact))]
    public class GetContact : BaseTenantGetCmdLet<Contact, CompanyService>
    {

        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
           HelpMessage = "Filter by Company Id",
          ValueFromPipelineByPropertyName = true)]
        public Guid Id { get; set; }


        protected override void ProcessRecord()
        {

            var company = GetCompany.GetOne(Id, Connection);

            if (company != null)
            {

                WriteObject(company.Contacts);
            }

        }

    

        
        
    }
}
