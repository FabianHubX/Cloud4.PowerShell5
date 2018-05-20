using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module.Models
{
    public class ConnectionResult
    {
        public string UserName { get; set; }

        public Uri LogonUrl { get; set; }
        public Uri ApiUrl { get; set; }

        public Guid TenantId { get; set; }

    }
}
