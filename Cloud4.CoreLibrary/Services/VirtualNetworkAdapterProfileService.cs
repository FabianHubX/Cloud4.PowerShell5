using Cloud4.CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class VirtualNetworkAdapterProfileService : BaseService<VirtualNetworkAdapterProfile, VirtualNetworkAdapterProfile, VirtualNetworkAdapterProfile>
    {
        public VirtualNetworkAdapterProfileService()
        {

        }
        public VirtualNetworkAdapterProfileService(Connection con) : base(con)
        {
            this.Entity = "VirtualNetworkAdapterProfiles";
         
        }


    }
}
