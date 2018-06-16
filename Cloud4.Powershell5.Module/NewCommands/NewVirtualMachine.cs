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
    [Cmdlet(VerbsCommon.New, "Cloud4VM")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualMachine : BaseTenantNewCmdLet<VirtualMachine, VirtualMachineService, CreateVirtualMachine>
    {
        private string _vMProfile;
        private string _oSDiskProfile;
        private string _nICProfile;
        private Guid _availabliltySet;
        private string _newavailabliltySetName;
        private List<CreateVirtualDisk> _dataDiskProfile;
        private VirtualMachineSetting _oSSettings;
        private bool _enableremoteaccess;
        private bool _enableinboundvnettraffic;
        private bool _enableinternetaccess;

        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            HelpMessage = "Name of the new Virtual Machine",
            ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }

        [Parameter(
           Mandatory = true,
           Position = 1,
           ValueFromPipeline = true,
            HelpMessage = "Virtual Datacenter where the Virtual Machine get created",
           ValueFromPipelineByPropertyName = true)]

        public Guid VirtualDatacenterId { get; set; }

        [Parameter(
         Mandatory = true,
         Position = 2,
         ValueFromPipeline = true,
          HelpMessage = "Virtual SubNet where the Virtual Machine get created",
         ValueFromPipelineByPropertyName = true)]

        public Guid VirtualSubNetId { get; set; }

        [Parameter(
           Mandatory = true,
           Position = 3,
           ValueFromPipeline = true,
            HelpMessage = "Selected OS Image",
           ValueFromPipelineByPropertyName = true)]

        public Guid ImageId { get; set; }

        [Parameter(
          Mandatory = true,
          Position = 4,
          ValueFromPipeline = true,
           HelpMessage = "Your VM Profile",
          ValueFromPipelineByPropertyName = true)]

        public string VmProfile { get => _vMProfile; set => _vMProfile = value; }

        [Parameter(
          Mandatory = true,
          Position = 5,
          ValueFromPipeline = true,
           HelpMessage = "Your OS Disk Profile",
          ValueFromPipelineByPropertyName = true)]

        public string OsDiskProfile { get => _oSDiskProfile; set => _oSDiskProfile = value; }

        [Parameter(
          Mandatory = true,
          Position = 6,
          ValueFromPipeline = true,
           HelpMessage = "Your Virtual Network Adapter Profile",
          ValueFromPipelineByPropertyName = true)]

        public string NicProfile { get => _nICProfile; set => _nICProfile = value; }

        [Parameter(
         Mandatory = false,
         Position = 7,
         ValueFromPipeline = true,
          HelpMessage = "Your Virtual Data Disk Profile",
         ValueFromPipelineByPropertyName = true)]

        public List<CreateVirtualDisk> DataDiskProfile { get => _dataDiskProfile; set => _dataDiskProfile = value; }


        [Parameter(
         Mandatory = true,
         Position = 8,
         ValueFromPipeline = true,
          HelpMessage = "Your OS Settings",
         ValueFromPipelineByPropertyName = true)]

        public VirtualMachineSetting OsSettings { get => _oSSettings; set => _oSSettings = value; }

        [Parameter(
        Mandatory = false,
        Position = 9,
        ValueFromPipeline = true,
         HelpMessage = "Your AvailabilitySet",
        ValueFromPipelineByPropertyName = true)]

        public Guid AvailabilitySetId { get => _availabliltySet; set => _availabliltySet = value; }

        [Parameter(
      Mandatory = false,
      Position = 10,
      ValueFromPipeline = true,
       HelpMessage = "New AvailabilitySet Name",
      ValueFromPipelineByPropertyName = true)]

        public string NewAvailabilitySetName { get => _newavailabliltySetName; set => _newavailabliltySetName = value; }


        [Parameter(
            Mandatory = false,
            Position = 11,
            ValueFromPipeline = true,
            HelpMessage = "EnableRemoteAccess",
            ValueFromPipelineByPropertyName = true)]

        public SwitchParameter EnableRemoteAccess { get => _enableremoteaccess; set => _enableremoteaccess = value; }


        [Parameter(
            Mandatory = false,
            Position = 12,
            ValueFromPipeline = true,
            HelpMessage = "EnableInternetAccess",
            ValueFromPipelineByPropertyName = true)]

        public SwitchParameter EnableInternetAccess { get => _enableinternetaccess; set => _enableinternetaccess = value; }


        [Parameter(
            Mandatory = false,
            Position = 13,
            ValueFromPipeline = true,
            HelpMessage = "New AvailabilitySet Name",
            ValueFromPipelineByPropertyName = true)]

        public SwitchParameter EnableInOutboundVNetTraffic { get => _enableinboundvnettraffic; set => _enableinboundvnettraffic = value; }

     

        [Parameter(
        Mandatory = false,
        Position = 14,
        ValueFromPipeline = true,
         HelpMessage = "Wait Job Finished",
        ValueFromPipelineByPropertyName = true)]

        public SwitchParameter Wait { get; set; }
        



        protected override void ProcessRecord()
        {


            var subnet = GetSpecial<VirtualSubNet, VirtualSubNetService>(Connection, VirtualSubNetId);



            if (_dataDiskProfile == null)
            {
                _dataDiskProfile = new List<CreateVirtualDisk>();
            }


            var virtualniclist = new List<CreateVirtualNetworkAdapter>();
            virtualniclist.Add(new CreateVirtualNetworkAdapter
            {
                IpAddress = subnet.NextFreeIpAddress,
                Name = "Primary",               
                SubNetId = VirtualSubNetId,
                VirtualNetworkAdapterProfileName = _nICProfile
            });



            CreateAvailabilitySetVM asid = null;


            if (_availabliltySet == Guid.Empty && !string.IsNullOrEmpty(_newavailabliltySetName))
            {
                asid = new CreateAvailabilitySetVM { Name = _newavailabliltySetName };
            }
            else if (_availabliltySet != Guid.Empty)
            {
                asid = new CreateAvailabilitySetVM { Id = _availabliltySet };
            }

            var newvm = new CreateVirtualMachine
            {
                Name = Name,
                VirtualDatacenterId = VirtualDatacenterId,
                AvailabilitySet = asid,
                ImageId = ImageId,
                VirtualMachineProfileName = _vMProfile,
                OsDisk = new CreateVirtualDisk { Name = "OS", VirtualDiskProfileName = _oSDiskProfile },
                NetworkInterfaces = virtualniclist,
                DataDisks = _dataDiskProfile,
                SetupSetting = _oSSettings,
                EnableInOutboundVNetTraffic = _enableinboundvnettraffic,
                EnableInternetAccess = _enableinternetaccess,
                EnableRemoteAccess = _enableremoteaccess

            };

            var job = Create(Connection, newvm);


            if (Wait)
            {
                WriteObject(WaitJobFinished(job.Id, Connection));
            }
            else
            {
                WriteObject(job);
            }

        }
        
        
    }
}
