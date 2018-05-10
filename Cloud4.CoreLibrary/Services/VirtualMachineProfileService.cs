// Copyright (c) HIAG Data AG. All Rights Reserved. Licensed under the GNU License.  See License.txt
using Cloud4.CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class VirtualMachineProfileService : BaseService<VirtualMachineProfile, VirtualMachineProfile, VirtualMachineProfile>
    {
        public VirtualMachineProfileService()
        {

        }
        public VirtualMachineProfileService(Connection con) : base(con)
        {
            this.Entity = "VirtualMachineProfiles";
            
        }


    }
}
