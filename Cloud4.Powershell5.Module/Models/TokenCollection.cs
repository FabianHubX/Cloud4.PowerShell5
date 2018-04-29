using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module
{
    public static class TokenCollection
    {
        private static Dictionary<Guid, object> Tokens = new Dictionary<Guid, object>();

        public static void Add(Guid runspId, object token)
        {
            //Console.WriteLine("Add Token to Runspace Id: " + runspId.ToString());
            Tokens.Add(runspId, token);
        }

        public static void Replace(Guid runspId, object token)
        {
            if (Tokens.ContainsKey(runspId))
            {
                Tokens.Remove(runspId);
            }
            Tokens.Add(runspId, token);
        }

        public static void Remove(Guid runspId)
        {
            Tokens.Remove(runspId);
            
        }

        public static object Get(Guid runspId)
        {
            if (Tokens.ContainsKey(runspId))
            {
                return Tokens[runspId];
            }

            return null;
        }
    }
}
