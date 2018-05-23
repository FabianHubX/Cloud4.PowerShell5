using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualGatewayNetworkConnection
    {
        public Guid Id { get; set; }
        public Guid VirtualGatewayId { get; set; }
        public string DestinationIpAddress { get; set; }
        public List<string> DestinationPrefix { get; set; }
        public string AuthenticationMethod { get; set; }
        public string MainModeDiffieHellmanGroup { get; set; }
        public string MainModeIntegrityAlgorithm { get; set; }
        public string MainModeEncryptionAlgorithm { get; set; }
        public int MainModeSALifeTimeSeconds { get; set; }
        public int MainModeSALifeTimeKiloBytes { get; set; }
        public string QuickModePerfectForwardSecrecy { get; set; }
        public string QuickModeCipherTransformationConstant { get; set; }
        public string QuickModeAuthenticationTransformationConstant { get; set; }
        public int QuickModeSALifeTimeSeconds { get; set; }
        public int QuickModeSALifeTimeKiloBytes { get; set; }
        public int QuickModeIdleDisconnectSeconds { get; set; }
    }
}
