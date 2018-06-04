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
    [Cmdlet(VerbsCommon.New, "Cloud4Company")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewCompany : BaseTenantNewCmdLet<Company, CompanyService, Company>
    {
       

        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public string DisplayName { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 1,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public string LegalName { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 2,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public CompanyCurrency BillingCurrency { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 3,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public CompanyType CompanyType { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 4,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public CompanyLanguage Language{ get; set; }
        [Parameter(
            Mandatory = true,
            Position = 5,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public string ERPAddressNo { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 6,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public string CRMId { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 7,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public string ChamberOfCommerceNo { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 8,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public string VatNo { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 9,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public string BusinessPhone { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 10,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public string Fax { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 11,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public string Email { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 12,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public string Street1 { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 13,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public string Street2 { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 14,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public string PostalCode { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 15,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public string Town { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 16,
            ValueFromPipeline = true,
            HelpMessage = "",
            ValueFromPipelineByPropertyName = true)]
        public string Country { get; set; }


        [Parameter(
      Mandatory = true,
      Position = 17,
      ValueFromPipeline = true,
      HelpMessage = "",
      ValueFromPipelineByPropertyName = true)]
        public List<Contact> Contacts { get; set; }




        [Parameter(
      Mandatory = false,
      Position = 2,
      ValueFromPipeline = true,
       HelpMessage = "Wait Job Finished",
      ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }


        protected override void ProcessRecord()
        {



            if (Contacts != null)
            {
                if (Contacts.Count == 3 & Contacts.Any(x => x.ContactType == "Billing") & Contacts.Any(y => y.ContactType == "Admin") & Contacts.Any(z => z.ContactType == "Emergency"))
                {
                    var company = new Company
                    {
                        DisplayName = DisplayName,
                        Email = Email,
                        ErpAddressNo = ERPAddressNo,
                        BillingCurrency = BillingCurrency.ToString(),
                        BusinessPhone = BusinessPhone,
                        ChamberOfCommerceNo = ChamberOfCommerceNo,
                        CompanyType = CompanyType.ToString(),
                        Contacts = Contacts,
                        Country = Country,
                        CrmId = CRMId,
                        LanguageId = Language.ToString(),
                        Fax = Fax,
                        Street1 = Street1,
                        Street2 = Street2,
                        LegalName = LegalName,
                        PostalCode = PostalCode,
                        Town = Town,
                        VatNo = VatNo
                    };

                    var job = Create(Connection, company);


                    if (Wait)
                    {
                        WriteObject(WaitJobFinished(job.Id, Connection));


                    }
                    else
                    {
                        WriteObject(job);
                    }
                }

            }


        }

        
        
    }
}
