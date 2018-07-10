// Copyright (c) HIAG Data AG. All Rights Reserved. Licensed under the GNU License.  See License.txt
using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class VirtualFirewallRuleService : BaseVirtualFirewallService<VirtualFirewallRule, CreateVirtualFirewallRule, VirtualFirewallRule>
    {
        public VirtualFirewallRuleService()
        {

        }
        public VirtualFirewallRuleService(Connection con, Guid VirtualGatewayId) : base(con, VirtualGatewayId)
        {
            this.Entity = "VirtualFirewalls";
            this.SubEntity = "rules";
           
        }





    }
}
