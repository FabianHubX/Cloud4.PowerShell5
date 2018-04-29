using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualDatacenter
    {
        public Guid Id { get; set; }
        public Guid RegionId { get; set; }

        public Guid TenantId { get; set; }

        public string Name { get; set; }
                
    }
}
