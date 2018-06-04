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
    [Cmdlet(VerbsCommon.Remove, "Cloud4vDC")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class RemoveVirtualDC : BaseTenantRemoveCmdLet<VirtualDatacenter, VirtualDataCenterService>
    {
        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
          ValueFromPipelineByPropertyName = true)]
      
        public Guid Id  { get; set; }

                [Parameter(
          Mandatory = false,
          Position = 1,
          ValueFromPipeline = true,
          HelpMessage = "Force Delete all Child Objects",
          ValueFromPipelineByPropertyName = true)]

        public bool Force { get; set; }

        [Parameter(
          Mandatory = false,
          Position = 2,
          ValueFromPipeline = true,
          HelpMessage = "Wait Job Finished",
          ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }
        


        protected override void ProcessRecord()
        {

            if (Force)
            {
                WriteObject(RemoveForce(Id, Connection));
            }
            else
            {

                var subnet = GetVirtualNet.GetbyvDCAll(Id, Connection);             

                if (subnet.Any())
                {
                    throw new RemoteException("ou can not delete this virtual Datacenter as virtual Networks are related too.");

                }
                 WriteObject(Remove(Id, Connection, Wait));

            }



        }

     

        public static CoreLibrary.Models.Job RemoveForce(Guid Id, Connection con)
        {
            CoreLibrary.Models.Job job;

            bool IsSuccessfull = true;

            var vnets = GetVirtualNet.GetbyvDCAll(Id, con);
            foreach (var vm in vnets)
            {
                job = RemoveVirtualNet.RemoveForce(vm.Id, con);
                if (job == null)
                {
                    IsSuccessfull = false;
                }
                else if (job.State == "failed")
                {
                    IsSuccessfull = false;
                }
            }

            if (IsSuccessfull)
            {
                return Remove(Id, con, true);
            }

            return null;
        }


    }
}
