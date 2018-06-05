// Copyright (c) HIAG Data AG. All Rights Reserved. Licensed under the GNU License.  See License.txt
using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class AvailabilitySetService : BaseTenantService<AvailabilitySet, CreateAvailabilitySet, AvailabilitySet>
    {
        public AvailabilitySetService()
        {

        }
        public AvailabilitySetService(Connection con) : base(con)
        {
            this.Entity = "AvailabilitySets";
         
        }

    }
}
