﻿using Cloud4.CoreLibrary.Models;
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
    [Cmdlet(VerbsCommon.Remove, "Cloud4vGWNetConnection")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class RemoveVirtualGatawayNetworkConnection : BaseVirtualGatewayRemoveCmdLet<VirtualGatewayNetworkConnection, VirtualGatewayNetworkConnectionService>
    {
        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
          ValueFromPipelineByPropertyName = true)]
     
        public Guid Id { get; set; }

        [Parameter(
         Mandatory = true,
         Position = 1,
         ValueFromPipeline = true,
         ValueFromPipelineByPropertyName = true)]

        public Guid VirtualGatewayId { get; set; }


        [Parameter(
        Mandatory = false,
        Position = 2,
        ValueFromPipeline = true,
        HelpMessage = "Wait Job Finished",
        ValueFromPipelineByPropertyName = true)]

        public SwitchParameter Wait { get; set; }




        protected override void ProcessRecord()
        {
            WriteObject(Remove(Id, Connection, VirtualGatewayId, Wait));
        }

      
    }
}
