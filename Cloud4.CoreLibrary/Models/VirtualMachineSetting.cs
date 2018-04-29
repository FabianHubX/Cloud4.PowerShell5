using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualMachineSetting
    {
        public string AdminUserPassword { get; set; }
        public string AdminUserName { get; set; }
        public string TimeZone { get; set; }
        public string ProductKey { get; set; }
        public string RegisteredOrganization { get; set; }

        public string RegisteredOwner { get; set; }

        public bool JoinDomain { get; set; }

        public string DomainJoinDomain { get; set; }
        public string DomainJoinPassword { get; set; }
        public string DomainJoinUsername { get; set; }
        public string Domain { get; set; }
        public string DomainOu { get; set; }
        public string SshKey { get; set; }


    }
}
