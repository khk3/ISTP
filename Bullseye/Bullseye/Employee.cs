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
        public int PositionID { get; set; }
        public int SiteID { get; set; }
        public bool Locked { get; set; }
        public string UserName { get; set; }
        public string Notes { get; set; }

        public Employee(string pw, string fn, string ln, string email, bool active, int posn, int site, bool locked, string userName, string notes)
        {            
            Password= pw;
            FirstName= fn;
            LastName= ln;
            Email = email;
            Active = active;
            PositionID = posn;
            SiteID= site;
            Locked = locked;
            UserName = userName;
            Notes = notes;
        }

        public Employee(int empID,string pw, string fn, string ln, string email, bool active, int posn, int site, bool locked, string userName, string notes)
        {
            EmployeeID = empID;
            Password = pw;
            FirstName = fn;
            LastName = ln;
            Email = email;
            Active = active;
            PositionID = posn;
            SiteID = site;
            Locked = locked;
            UserName = userName;
            Notes = notes;
        }


    }
}
