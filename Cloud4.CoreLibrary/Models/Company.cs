using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class Company
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public string LegalName { get; set; }

        public string BillingCurrency { get; set; }

        public string CompanyType { get; set; }

        public string LanguageId { get; set; }
        public string ErpAddressNo { get; set; }

        public string CrmId { get; set; }

        public string ChamberOfCommerceNo { get; set; }

        public string VatNo { get; set; }

        public string BusinessPhone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string PostalCode { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public List<Contact> Contacts { get; set; }

        public List<string> CompanyTypesAllowedForCreation { get; set; }
    }
}
