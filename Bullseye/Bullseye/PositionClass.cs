using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullseye
{
    public class PositionClass
    {
       public int PositionID { get; set; }
        public string PermissionLevel { get; set; }
       

       

        public PositionClass() { }
        public PositionClass(int positionID, string permissionLevel)
        {
            PositionID = positionID;
            PermissionLevel = permissionLevel;
        }

        public bool CheckUser(int positionID)
        {
            MySqlClass m=new MySqlClass();
            PositionClass[] positionArr= m.GetAllPositions();

            return true;
            
        }
  
    }

}
