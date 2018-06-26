using Cloud4.CoreLibrary.Models;
using Cloud4.CoreLibrary.Services;
using Cloud4.Powershell5.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module
{
    [Cmdlet(VerbsCommon.Get, "Cloud4vLB")]
    [OutputType(typeof(VirtualLoadBalancer))]
    public class GetVirtualLoadBalancer : BaseTenantGetCmdLet<VirtualLoadBalancer, VirtualLoadBalancerService>
    {
       
      
        [Parameter(
           Mandatory = false,
           Position = 0,
           ValueFromPipeline = true,
            HelpMessage = "Filter by vLoadBalancer Id",
           ValueFromPipelineByPropertyName = true)]
      
        public Guid Id { get; set; }



        [Parameter(
    Mandatory = false,
    Position = 1,
    ValueFromPipeline = true,
     HelpMessage = "Filter by Virtual LoadBalancer Name",
    ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }

        [Parameter(
        Mandatory = false,
        Position = 2,
        ValueFromPipeline = true,
           HelpMessage = "Filter by Virtual Datacenter Id",
        ValueFromPipelineByPropertyName = true)]

        public Guid VirtualDatacenterId { get; set; }
   

        protected override void ProcessRecord()
        {
            if (!string.IsNullOrEmpty(Name))
            {

                var pattern = new WildcardPattern(Name);
                GetAll(Connection).Where(x => pattern.IsMatch(x.Name)).ToList().ForEach(WriteObject);

            }
            else if (VirtualDatacenterId != Guid.Empty)
            {

                GetAll(Connection).Where(x => x.VirtualDatacenterId == VirtualDatacenterId).ToList().ForEach(WriteObject);

            }
            else if (Id == Guid.Empty)
            {
                GetAll(Connection).ForEach(WriteObject);
            }
            else
            {
                var lborg = GetOne(Id, Connection);
                var lb = new ExtendedVirtualLoadBalancer();
                lb.Id = lborg.Id;
                lb.Name = lborg.Name;
                lb.VirtualDatacenterId = lborg.VirtualDatacenterId;
                lb.BackendAddressPools = new List<VirtualLoadBalancerBackEndPool>();
                lb.FrontEndIPConfigurations = new List<VirtualLoadBalancerFrontEndIPConfigurations>();
                lb.InboundNatRules = new List<VirtualLoadBalancerInboundNatRule>();
                lb.LoadBalancingRules = new List<VirtualLoadBalancerRule>();
                lb.Probes = new List<VirtualLoadBalancerProbe>();

                foreach (var item in lborg.BackendAddressPools)
                {
                   lb.BackendAddressPools.Add(GetVirtualLoadBalancerBackEndPool.GetOne(item, Connection, lb.Id));

                }

                foreach (var item in lborg.FrontEndIPConfigurations)
                {
                    lb.FrontEndIPConfigurations.Add(GetVirtualFrontEndIPConfigurations.GetOne(item, Connection, lb.Id));

                }

                foreach (var item in lborg.InboundNatRules)
                {
                    lb.InboundNatRules.Add(GetVirtualLoadBalancerInboundNatRule.GetOne(item, Connection, lb.Id));

                }

                foreach (var item in lborg.LoadBalancingRules)
                {
                    lb.LoadBalancingRules.Add(GetVirtualLoadBalancerRule.GetOne(item, Connection, lb.Id));

                }

                foreach (var item in lborg.Probes)
                {
                    lb.Probes.Add(GetVirtualLoadBalancerProbe.GetOne(item, Connection, lb.Id));

                }

            


                WriteObject(lb);
            }

        }

        public static List<VirtualLoadBalancer> GetbyvDCAll(Guid vDCId, Connection con)
        {
            return GetAll(con).Where(x => x.VirtualDatacenterId == vDCId).ToList();
        }

    }
}
