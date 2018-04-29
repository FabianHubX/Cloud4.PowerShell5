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
    [Cmdlet(VerbsData.Update, "Cloud4vFirewall")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class UpdateVirtualFirewall : BaseCmdLet
    {
        [Parameter(
     Mandatory = true,
     Position = 0,
     ValueFromPipeline = true,
      HelpMessage = "Filter by vFirewall Id",
     ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

        [Parameter(
     Mandatory = false,
     Position = 1,
     ValueFromPipeline = true,
       HelpMessage = "Name of the virtual Firewall",
     ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }


        [Parameter(
     Mandatory = false,
     Position = 2,
     ValueFromPipeline = true,
       HelpMessage = "New Ruleset of the virtual Firewall",
     ValueFromPipelineByPropertyName = true)]

        public List<VirtualFirewallRule> Rules { get; set; }


        [Parameter(
      Mandatory = false,
      Position = 3,
      ValueFromPipeline = true,
       HelpMessage = "Wait Job Finished",
      ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }

        private VirtualFirewallService service { get; set; }



        protected override void ProcessRecord()
        {

            service = new VirtualFirewallService(Connection);
         

            try
            {

               
                Task<Cloud4.CoreLibrary.Models.VirtualFirewall> callTaskvNet = Task.Run(() => service.GetAsync(Id));

                callTaskvNet.Wait();
                var vnet = callTaskvNet.Result;


                var newfirewall = new Cloud4.CoreLibrary.Models.UpdateVirtualFirewall {  Name = vnet.Name, Rules = vnet.Rules};


                bool IsChanged = false;

                if (!string.IsNullOrEmpty(Name))
                {
                    newfirewall.Name = Name;
                    IsChanged = true;
                }
                
                if (Rules != null)
                {
                    newfirewall.Rules = Rules;
                    IsChanged = true;
                }

                if (IsChanged)
                {

                    Task<CoreLibrary.Models.Job> callTask = Task.Run(() => service.UpdateAsync(Id, newfirewall));

                    callTask.Wait();
                    var job = callTask.Result;
                    if (Wait)
                    {
                        WaitJobFinished(job.Id);

                        Task<List<VirtualFirewall>> callTasklist = Task.Run(() => service.AllAsync());

                        callTasklist.Wait();
                        var virtualnetworks = callTasklist.Result;

                        WriteObject(virtualnetworks.FirstOrDefault(x => x.Id == job.ResourceId));

                    }
                    else
                    {
                        WriteObject(job);
                    }

                }
            }
            catch (Exception e)
            {
                throw new RemoteException("An API Error has happen");
            }


        }

        protected override void EndProcessing()
        {

        }
    }
}
