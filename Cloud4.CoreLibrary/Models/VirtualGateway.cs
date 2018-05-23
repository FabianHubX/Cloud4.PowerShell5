using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualGateway
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PublicIpAddress { get; set; }
        public Guid VirtualDatacenterId { get; set; }
        public Guid VirtualSubnetId { get; set; }
        public List<Guid> NetworkConnections { get; set; }
    }
}

