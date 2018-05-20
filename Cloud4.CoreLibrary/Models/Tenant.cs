using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud4.CoreLibrary.Models
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid PlatformId { get; set; }
    }
}
