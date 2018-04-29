using Cloud4.CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class ImageService : BaseTenantService<Image, Image, Image>
    {
        public ImageService()
        {

        }

        public ImageService(Connection con) : base(con)
        {
            this.Entity = "Images";
         
        }

    }
}
