using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class VirtualDiskService : BaseTenantService<VirtualDisk, CreateVirtualDisk, VirtualDisk>
    {
        public VirtualDiskService()
        {

        }

        public VirtualDiskService(Connection con) : base(con)
        {
            this.Entity = "VirtualDisks";
         
        }


    }
}
