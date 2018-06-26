using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class Connection
    {
        public string UserName { get; set; }

        public string PassWord { get; set; }

        public Uri LogonUrl { get; set; }
        public Uri ApiUrl { get; set; }

        public Guid TenantId { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpiresAt { get; set; }

    }
}
