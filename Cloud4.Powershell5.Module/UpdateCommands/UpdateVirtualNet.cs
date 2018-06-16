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
    [Cmdlet(VerbsData.Update, "Cloud4vNet")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class UpdateVirtualNetwork : BaseTenantUpdateCmdLet< VirtualNetwork, VirtualNetworkService, VirtualNetwork>
    {
        [Parameter(
     Mandatory = true,
     Position = 0,
     ValueFromPipeline = true,
      HelpMessage = "Filter by vNet Id",
               ParameterSetName = "Default",
     ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

        [Parameter(
     Mandatory = true,
     Position = 1,
     ValueFromPipeline = true,
               ParameterSetName = "Default",
       HelpMessage = "Name of the virtual Network",
     ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }


        [Parameter(
  Mandatory = true,
  Position = 4,
  ValueFromPipeline = true,
    HelpMessage = "New Set of DNS Servers for this Virtual Network",
               ParameterSetName = "Default",
  ValueFromPipelineByPropertyName = true)]

        public List<string> DnsServers { get; set; }

        [Parameter(
 Mandatory = false,
 Position = 3,
 ValueFromPipeline = true,
  HelpMessage = "Update DNS Server on all attached NetAdapter",
            ParameterSetName = "DNS",
 ValueFromPipelineByPropertyName = true)]

        public SwitchParameter UpdateDNSonAllNetAdapters { get; set; }


        [Parameter(
      Mandatory = false,
      Position = 2,
      ValueFromPipeline = true,
       HelpMessage = "Wait Job Finished",
      ValueFromPipelineByPropertyName = true)]

        public SwitchParameter Wait { get; set; }

 



        protected override void ProcessRecord()
        {

            if (UpdateDNSonAllNetAdapters)
            {
                var vnet = Get(Connection, Id);
                var vsubnets = GetVirtualSubNet.GetByvNetAll(Id, Connection);



                foreach (var vsubnet in vsubnets)
                {
                    var netadapters = GetVirtualNetAdapter.GetbyvSubnetAll(vsubnet.Id, Connection);

                    foreach (var netadapter in netadapters)
                    {
                        var job = UpdateVirtualNetAdapter.UpdateDNSonNetAdapter(netadapter.Id, vnet.DnsServers, Connection);

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
            else
            {
                var vnet = Get(Connection, Id);



                if (!string.IsNullOrEmpty(Name))
                {
                    vnet.Name = Name;
                }
                if (DnsServers != null)
                {
                    vnet.DnsServers = DnsServers.ToArray();
                }

                var job = Update(Connection, Id, vnet);

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
}
