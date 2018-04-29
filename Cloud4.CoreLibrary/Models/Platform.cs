using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud4.CoreLibrary.Models
{
    public class Platform
    {
        public Guid Id { get; set; }
        public string LogoFileName { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Details { get; set; }
        public string MoreInfoText { get; set; }
        public string MoreInfoLink { get; set; }
        public bool AllowIaaS { get; set; }
        
        }
    }
