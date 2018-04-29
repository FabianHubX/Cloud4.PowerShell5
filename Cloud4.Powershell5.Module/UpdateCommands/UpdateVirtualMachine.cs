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
    [Cmdlet(VerbsData.Update, "Cloud4VM")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class UpdateVirtualMachine : BaseCmdLet
    {
        [Parameter(
     Mandatory = true,
     Position = 0,
     ValueFromPipeline = true,
      HelpMessage = "Filter by Virutal Machine Id",
     ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

        [Parameter(
     Mandatory = false,
     Position = 1,
     ValueFromPipeline = true,
       HelpMessage = "Profile of the virtual Maschine",
     ValueFromPipelineByPropertyName = true)]

        public string VMProfile { get; set; }

        [Parameter(
Mandatory = false,
Position = 2,
ValueFromPipeline = true,
HelpMessage = "Enable Remote Access for the virtual Maschine",
ValueFromPipelineByPropertyName = true)]

        public bool? EnableRemoteAccess { get; set; }

        [Parameter(
Mandatory = false,
Position = 3,
ValueFromPipeline = true,
HelpMessage = "Enable Internet Access for the virtual Maschine",
ValueFromPipelineByPropertyName = true)]

        public bool? EnableInternetAccess { get; set; }



        [Parameter(
      Mandatory = false,
      Position = 4,
      ValueFromPipeline = true,
       HelpMessage = "Wait Job Finished",
      ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }

        private VirtualMachineService service { get; set; }



        protected override void ProcessRecord()
        {

            service = new VirtualMachineService(Connection);


            try
            {

           
                Task<Cloud4.CoreLibrary.Models.VirtualMachine> callTaskvNet = Task.Run(() => service.GetAsync(Id));

                callTaskvNet.Wait();
                var vnet = callTaskvNet.Result;

                if (!string.IsNullOrEmpty(VMProfile))
                {
                    vnet.VirtualMachineProfile = VMProfile;




                    Task<CoreLibrary.Models.Job> callTask = Task.Run(() => service.UpdateAsync(Id, vnet));

                    callTask.Wait();
                    var job = callTask.Result;
                    if (Wait)
                    {
                        WaitJobFinished(job.Id);

                        Task<List<VirtualMachine>> callTasklist = Task.Run(() => service.AllAsync());

                        callTasklist.Wait();
                        var virtualnetworks = callTasklist.Result;

                        WriteObject(virtualnetworks.FirstOrDefault(x => x.Id == job.ResourceId));

                    }
                    else
                    {
                        WriteObject(job);
                    }

                }

                if (EnableRemoteAccess.HasValue)
                { 
                    string action = "";
                    if (EnableRemoteAccess.Value)
                    {
                    action = "EnableRemoteAccess";
                    }
                    else
                    {
                    action = "DisableRemoteAccess";
                    }

                    string vmid = Id.ToString("D").ToLower();
                    Task<Cloud4.CoreLibrary.Models.Job> callTask = Task.Run(() => service.ActionAsync(vmid, new CoreLibrary.Models.ActionParameter { Action = action }));

                    callTask.Wait();
                    var job = callTask.Result;
                    if (Wait)
                    {
                        WaitJobFinished(job.Id);
                        Task<VirtualMachine> callTasklist = Task.Run(() => service.GetAsync(job.ResourceId));

                        callTasklist.Wait();
                        var virtualnetworks = callTasklist.Result;

                        WriteObject(virtualnetworks);
                    }
                    else
                    {
                        WriteObject(job);
                    }

                }
                if (EnableInternetAccess.HasValue)
                {
                    string action = "";
                    if (EnableRemoteAccess.Value)
                    {
                        action = "EnableInternetAccess";
                    }
                    else
                    {
                        action = "DisableInternetAccess";
                    }

                    string vmid = Id.ToString("D").ToLower();
                    Task<Cloud4.CoreLibrary.Models.Job> callTask = Task.Run(() => service.ActionAsync(vmid, new CoreLibrary.Models.ActionParameter { Action = action }));

                    callTask.Wait();
                    var job = callTask.Result;
                    if (Wait)
                    {
                        WaitJobFinished(job.Id);
                        Task<VirtualMachine> callTasklist = Task.Run(() => service.GetAsync(job.ResourceId));

                        callTasklist.Wait();
                        var virtualnetworks = callTasklist.Result;

                        WriteObject(virtualnetworks);
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
