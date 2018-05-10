// Copyright (c) HIAG Data AG. All Rights Reserved. Licensed under the GNU License.  See License.txt
using Cloud4.CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud4.CoreLibrary.Services
{
    public class RegionService : BaseService<Region, Region, Region>
    {
        public RegionService()
        {

        }

        public RegionService(Connection con) : base(con)
        {
            this.Entity = "Regions";
          
        }


    }
}
