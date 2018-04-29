using Cloud4.CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud4.CoreLibrary.Services
{
    public class JobService : BaseService<Job, Job, Job>
    {
        public JobService()
        {

        }

        public JobService(Connection con) : base(con)
        {
            this.Entity = "Jobs";
         
        }


    }
}
