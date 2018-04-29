using Cloud4.CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud4.CoreLibrary.Services
{
    public class PlatformService : BaseService<Platform, Platform, Platform>
    {
        public PlatformService()
        {

        }
        public PlatformService(Connection con) : base(con)
        {
            this.Entity = "Platforms";
           
        }


    }
}
