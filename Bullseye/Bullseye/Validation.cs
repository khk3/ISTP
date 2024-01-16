using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bullseye
{
    public class Validation
    {
        public Validation() { }

        public bool ValidadePassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*[!@#$%^&*()-_+=])[a-zA-Z0-9!@#$%^&*()-_+=/\\]{8,}$";
            return Regex.IsMatch(password, pattern);           
        }

       
    
    }
}
