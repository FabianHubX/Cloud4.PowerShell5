using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class Contact
    {
        public Guid Id { get; set; }

        public string ContactType { get; set; }

        public string Name1 { get; set; }

        public string Name2 { get; set; }

        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string PostalCode { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string PhoneMobile { get; set; }

        public string Email { get; set; }

        public string InstantMessenger { get; set; }

        public string ChamberOfCommerceNo { get; set; }

        public string VatNo { get; set; }

        public string BillingReference { get; set; }
    }
}
