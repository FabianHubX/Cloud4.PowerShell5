﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class CreateAvailabilitySet
    {
      
        public string Name { get; set; }

        public Guid VirtualDatacenterId { get; set; }
    }
}
