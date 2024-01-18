using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullseye
{
    public class ConstantsClass
    {
        public static readonly string DefaultPassword = "P@ssw0rd-";
        public static readonly string DatabaseName = "bullseyedb2024";

        public static readonly string ScriptName = "Bullseye_DB2024_1.3.sql";

        //public static string[] UserPermissions = { "Create Back Order","Create Loss", "Create Report", "Create Store Order","Create Supplier Order"};

        public static readonly TimeSpan TimeToAutoLogout = TimeSpan.FromMinutes(20);
        public static bool Active = true;
        public static bool Locked = false;
    
    
    }
}
