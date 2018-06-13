using Cloud4.CoreLibrary.Models;
using Cloud4.CoreLibrary.Services;
using Cloud4.Powershell5.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module
{
    [Cmdlet(VerbsCommon.New, "Cloud4vGWNetConnection")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualGatewayNetworkConnection : BaseNewVirtualGatewayCmdLet<VirtualGatewayNetworkConnection, VirtualGatewayNetworkConnectionService, CreateVirtualGatewayNetworkConnection>
    {


        [Parameter(
        Mandatory = true,
        Position = 0,
        ValueFromPipeline = true,
        HelpMessage = "Id of the Virtual Gateway",
        ValueFromPipelineByPropertyName = true)]

        public Guid VirtualGatewayId { get; set; }


        [Parameter(
        Mandatory = true,
        Position = 1,
        ValueFromPipeline = true,
        HelpMessage = "Destination IP Address",
        ValueFromPipelineByPropertyName = true)]

        public string DestinationIpAddress { get; set; }

        [Parameter(
        Mandatory = true,
        Position = 2,
        ValueFromPipeline = true,
        HelpMessage = "Destination Preffixes",
        ValueFromPipelineByPropertyName = true)]

        public List<string> DestinationPrefix { get; set; }

        [Parameter(
        Mandatory = true,
        Position = 3,
        ValueFromPipeline = true,
        HelpMessage = "Protocol",
        ValueFromPipelineByPropertyName = true)]

        public VirtualGatewayParameters.IpSecAuthenticationMethod AuthenticationMethod { get; set; }

        [Parameter(
        Mandatory = true,
        Position = 4,
        ValueFromPipeline = true,
        HelpMessage = "DiffieHellmanGroup",
        ValueFromPipelineByPropertyName = true)]

        public VirtualGatewayParameters.IpSecDiffieHellmanGroup DiffieHellmanGroup { get; set; }


        [Parameter(
    Mandatory = true,
    Position = 5,
    ValueFromPipeline = true,
        HelpMessage = "EncryptionAlgorithm",
    ValueFromPipelineByPropertyName = true)]

        public VirtualGatewayParameters.IpSecEncryptionAlgorithm EncryptionAlgorithm { get; set; }

        [Parameter(
    Mandatory = true,
    Position = 6,
    ValueFromPipeline = true,
        HelpMessage = "IntegrityAlgorithm",
    ValueFromPipelineByPropertyName = true)]

        public VirtualGatewayParameters.IpSecIntegrityAlgorithm IntegrityAlgorithm { get; set; }

        [Parameter(
    Mandatory = true,
    Position = 7,
    ValueFromPipeline = true,
        HelpMessage = "AuthenticationTranformationConstant",
    ValueFromPipelineByPropertyName = true)]

        public VirtualGatewayParameters.IpSecAuthenticationTranformationConstant AuthenticationTranformationConstant { get; set; }

        [Parameter(
    Mandatory = true,
    Position = 8,
    ValueFromPipeline = true,
        HelpMessage = "CipherTransformationConstant",
    ValueFromPipelineByPropertyName = true)]

        public VirtualGatewayParameters.IpSecCipherTransformationConstant CipherTransformationConstant { get; set; }

        [Parameter(
        Mandatory = true,
        Position = 9,
        ValueFromPipeline = true,
        HelpMessage = "PerfectForwardSecrecy",
        ValueFromPipelineByPropertyName = true)]

        public VirtualGatewayParameters.IpSecPerfectForwardSecrecy PerfectForwardSecrecy { get; set; }

        [Parameter(
        Mandatory = true,
        Position = 10,
        ValueFromPipeline = true,
        HelpMessage = "MainModeSALifeTimeKiloBytes",
        ValueFromPipelineByPropertyName = true)]

        public int MainModeSALifeTimeKiloBytes { get; set; }

        [Parameter(
        Mandatory = true,
        Position = 11,
        ValueFromPipeline = true,
        HelpMessage = "MainModeSALifeTimeSeconds",
        ValueFromPipelineByPropertyName = true)]

        public int MainModeSALifeTimeSeconds { get; set; }


        [Parameter(
        Mandatory = true,
        Position = 12,
        ValueFromPipeline = true,
        HelpMessage = "QuickModeIdleDisconnectSeconds",
        ValueFromPipelineByPropertyName = true)]

        public int QuickModeIdleDisconnectSeconds { get; set; }

        [Parameter(
        Mandatory = true,
        Position = 13,
        ValueFromPipeline = true,
        HelpMessage = "QuickModeSALifeTimeSeconds",
        ValueFromPipelineByPropertyName = true)]

        public int QuickModeSALifeTimeSeconds { get; set; }

        [Parameter(
        Mandatory = true,
        Position = 14,
        ValueFromPipeline = true,
        HelpMessage = "QuickModeSALifeTimeKiloBytes",
        ValueFromPipelineByPropertyName = true)]

        public int QuickModeSALifeTimeKiloBytes { get; set; }

        [Parameter(
     Mandatory = true,
     Position = 15,
     ValueFromPipeline = true,
     HelpMessage = "Name",
     ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }

        [Parameter(
Mandatory = true,
Position = 16,
ValueFromPipeline = true,
HelpMessage = "SharedSecret",
ValueFromPipelineByPropertyName = true)]

        public string SharedSecret { get; set; }



        [Parameter(
        Mandatory = false,
        Position = 17,
        ValueFromPipeline = true,
        HelpMessage = "Wait Job Finished",
        ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }



        protected override void ProcessRecord()
        {

            var vgw = new CreateVirtualGatewayNetworkConnection
            {
                AuthenticationMethod = AuthenticationMethod.ToString(),
                DestinationIpAddress = DestinationIpAddress,
                DestinationPrefix = DestinationPrefix,
                MainModeDiffieHellmanGroup = DiffieHellmanGroup.ToString(),
                MainModeEncryptionAlgorithm = EncryptionAlgorithm.ToString(),
                MainModeIntegrityAlgorithm = IntegrityAlgorithm.ToString(),
                MainModeSALifeTimeKiloBytes = MainModeSALifeTimeKiloBytes,
                MainModeSALifeTimeSeconds = MainModeSALifeTimeSeconds,
                QuickModeAuthenticationTransformationConstant = AuthenticationTranformationConstant.ToString(),
                QuickModeCipherTransformationConstant = CipherTransformationConstant.ToString(),
                QuickModeIdleDisconnectSeconds = QuickModeIdleDisconnectSeconds,
                QuickModePerfectForwardSecrecy = PerfectForwardSecrecy.ToString(),
                QuickModeSALifeTimeKiloBytes = QuickModeSALifeTimeKiloBytes,
                QuickModeSALifeTimeSeconds = QuickModeSALifeTimeSeconds,
                Name = Name,
                SharedSecret = SharedSecret

            };

            var job = Create(Connection, vgw, VirtualGatewayId);


            if (Wait)
            {
                WriteObject(WaitJobFinished(job.Id, Connection,VirtualGatewayId));
            }
            else
            {
                WriteObject(job);
            }

        }



    }
}
