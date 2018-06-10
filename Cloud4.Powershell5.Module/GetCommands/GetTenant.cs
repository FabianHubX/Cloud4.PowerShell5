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
    [Cmdlet(VerbsCommon.Get, "Cloud4Tenant")]
    [OutputType(typeof(Tenant))]
    public class GetTenant : BaseTenantGetCmdLet<Tenant, TenantService>
    {



        [Parameter(
           Mandatory = false,
           Position = 0,
           ValueFromPipeline = true,
            HelpMessage = "Filter by Id",
           ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }
        [Parameter(
        Mandatory = false,
        Position = 1,
        ValueFromPipeline = true,
         HelpMessage = "Filter by Name",
        ValueFromPipelineByPropertyName = true)]

        public string FilterByName { get; set; }



        protected override void ProcessRecord()
        {
            if (!string.IsNullOrEmpty(FilterByName))
            {

                GetAll(Connection).Where(x => x.Name == FilterByName).ToList().ForEach(WriteObject);

            }
            else if (Id == Guid.Empty)
            {
                GetAll(Connection).ForEach(WriteObject);
            }
            else
            {
                WriteObject(GetOne(Id, Connection));
            }

        }

        
       
    }
}