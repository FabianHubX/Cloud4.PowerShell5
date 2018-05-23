using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module.Models
{
    public static class VirtualGatewayParameters
    {
        public enum IpSecAuthenticationMethod
        {
            PSK
        }

        public enum IpSecAuthenticationTranformationConstant
        {
            None,
            MD596,
            SHA196,
            SHA256128,
            GCMAES128,
            GCMAES192,
            GCMAES256
        }

        public enum IpSecCipherTransformationConstant
        {
            None,
            DES,
            DES3,
            AES128,
            AES192,
            AES256,
            GCMAES128,
            GCMAES192,
            GCMAES256
        }

        public enum IpSecDiffieHellmanGroup
        {
            Group1,
            Group2,
            Group14,
            ECP256,
            ECP384,
            Group24
        }

        public enum IpSecEncryptionAlgorithm
        {
            DES,
            DES3,
            AES128,
            AES192,
            AES256
        }

        public enum IpSecIntegrityAlgorithm
        {
            MD5,
            SHA1,
            SHA256,
            SHA384
        }

        public enum IpSecPerfectForwardSecrecy
        {
            None,
            PFS1,
            PFS2,
            PFS2048,
            ECP256,
            ECP384,
            PFSMM,
            PFS24
        }

        public enum TrafficSelectorProtocolId
        {
            UDP,
            TCP,
            ICMP
        }

        public enum TrafficSelectorType
        {
            IPv4,
            IPv6
        }
    }
}
