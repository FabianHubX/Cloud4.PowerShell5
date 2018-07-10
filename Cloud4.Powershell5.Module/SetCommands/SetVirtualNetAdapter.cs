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
    [Cmdlet(VerbsCommon.Set, "Cloud4vNetAdapter")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class SetVirtualNetAdapter : BaseTenantUpdateCmdLet<VirtualNetworkAdapter, VirtualNetworkAdapterService, Cloud4.CoreLibrary.Models.UpdateVirtualNetworkAdapter>
    {

        [Parameter(
     Mandatory = true,
     Position = 0,
     ValueFromPipeline = true,
      HelpMessage = "Filter by vNetAdapater Id",
     ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

        [Parameter(
         Mandatory = false,
         Position = 1,
         ValueFromPipeline = true,
          HelpMessage = "Virtual SubNet where the Virtual NetAdapter gets assigned too",
         ValueFromPipelineByPropertyName = true)]

        public Guid? VirtualSubNetId { get; set; }


        [Parameter(
         Mandatory = false,
         Position = 1,
         ValueFromPipeline = true,
          HelpMessage = "Virtual Firewall where the Virtual NetAdapter gets assigned too",
         ValueFromPipelineByPropertyName = true)]

        public Guid? VirtualFirewallId { get; set; }

        [Parameter(
           Mandatory = false,
           Position = 2,
           ValueFromPipeline = true,
            HelpMessage = "Your Virtual Network Adapter Profile",
           ValueFromPipelineByPropertyName = true)]

        public string NicProfile { get; set; }

        [Parameter(
   Mandatory = false,
   Position = 2,
   ValueFromPipeline = true,
    HelpMessage = "Your Virtual Network Adapter IP Address",
   ValueFromPipelineByPropertyName = true)]

        public string IpAddress { get; set; }

        [Parameter(
      Mandatory = false,
      Position = 2,
      ValueFromPipeline = true,
       HelpMessage = "Name for the Virtual Network Adapter",
      ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }


        [Parameter(
  Mandatory = false,
  Position = 4,
  ValueFromPipeline = true,
    HelpMessage = "New Set of DNS Servers for this Virtual Network Adapter",
  ValueFromPipelineByPropertyName = true)]

        public string[] DnsServers { get; set; }

        public SwitchParameter Wait { get; set; }


        protected override void ProcessRecord()
        {


            var orgnetadapter = Get(Connection, Id);

            var newnetadapter = new Cloud4.CoreLibrary.Models.UpdateVirtualNetworkAdapter();


            bool IsChanged = false;

            if (DnsServers != null)
            {
                newnetadapter.DnsServers = DnsServers;
                IsChanged = true;
            }
            else
            {
                newnetadapter.DnsServers = orgnetadapter.DnsServers;
            }

            if (!string.IsNullOrEmpty(Name))
            {
                newnetadapter.Name = Name;
                IsChanged = true;
            }
            else
            {
                newnetadapter.Name = orgnetadapter.Name;
            }

            if (!string.IsNullOrEmpty(IpAddress))
            {
                newnetadapter.IpAddress = IpAddress;
                IsChanged = true;
            }
            else
            {
                newnetadapter.IpAddress = orgnetadapter.IpAddress;
            }

            if (!string.IsNullOrEmpty(NicProfile))
            {
                newnetadapter.VirtualNetworkAdapterProfileName = NicProfile;
                IsChanged = true;
            }
            else
            {
                newnetadapter.VirtualNetworkAdapterProfileName = orgnetadapter.VirtualNetworkAdapterProfileName;
            }

            if (VirtualSubNetId.HasValue)
            {
                newnetadapter.SubNetId = VirtualSubNetId.Value;
                IsChanged = true;
            }
            else
            {
                newnetadapter.SubNetId = orgnetadapter.SubNetId;
            }

            if (VirtualFirewallId.HasValue)
            {
                if (VirtualFirewallId.Value != Guid.Empty)
                {
                    newnetadapter.VirtualFirewallAssignment = new VirtualFirewallAssignment { VirtualFirewallId = VirtualFirewallId.Value };
                }
                else
                {
                    newnetadapter.VirtualFirewallAssignment = new VirtualFirewallAssignment { VirtualFirewallId = null };
                }
                
                IsChanged = true;

            }
            else
            {
                newnetadapter.VirtualFirewallAssignment =  new VirtualFirewallAssignment { VirtualFirewallId =  orgnetadapter.VirtualFirewallId};
            }

            if (IsChanged)
            {

                var job = Update(Connection, Id, newnetadapter);

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

        public static Cloud4.CoreLibrary.Models.Job UpdateDNSonNetAdapter(Guid Id, string[] DnsServers, Connection con)
        {
            var orgnetadapter = Get(con, Id);

            var newnetadapter = new Cloud4.CoreLibrary.Models.UpdateVirtualNetworkAdapter();

            newnetadapter.DnsServers = DnsServers;
            newnetadapter.IpAddress = orgnetadapter.IpAddress;
            newnetadapter.Name = orgnetadapter.Name;
            newnetadapter.SubNetId = orgnetadapter.SubNetId;
            newnetadapter.VirtualFirewallAssignment = new VirtualFirewallAssignment { VirtualFirewallId = orgnetadapter.VirtualFirewallId };
            newnetadapter.VirtualNetworkAdapterProfileName = orgnetadapter.VirtualNetworkAdapterProfileName;

            return Update(con, Id, newnetadapter);

           
        }

    }
}
