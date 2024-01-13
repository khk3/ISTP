using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullseye
{
    public class PositionClass
    {
        public static readonly Dictionary<int, string> PositionNames = new Dictionary<int, string>
        {
            { 1, "Regional Manager" },
            { 2, "Financial Manager" },
            { 3, "Store Manager" },
            { 4, "Warehouse Manager" },
            { 5, "Trucking Delivery" },
            { 6, "Warehouse Employee" },
            { 99999999, "Administrator" }
        };

       

        public string Position(int posnID)
        {
            return PositionNames.TryGetValue(posnID, out var positionName) ? positionName : "Position Invalid";
        }

        public PositionClass() { }

  
    }

}
