using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullseye
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string PositionID { get; set; }
        public int SiteID { get; set; }
        public bool Locked { get; set; }
        public string Notes { get; set; }
    }
}
