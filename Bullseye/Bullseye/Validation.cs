using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bullseye
{
    public class Validation
    {
        public Validation() { }

        public bool ValidadePassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*[!@#$%^&*()-_+=])[a-zA-Z0-9!@#$%^&*()-_+=/\\]{8,}$";
            bool isMatch=false;
            try
            {
                isMatch= Regex.IsMatch(password, pattern);
            }
            catch(Exception e)
            {
                MessageBox.Show("Validate Password Failed. Error: "+e.Message, "Error- Validate Password");
            }
            return isMatch;
                     
        }
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes=null;
                try 
                { 
                   hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error password encryption. Error: " + e.Message, "Error- Password Encryption");
                }
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public static bool VerifyPassword(string enteredPassword, string HashedPassword)
        {               
            string enteredPasswordHash = null;
            try
            {
                enteredPasswordHash = HashPassword(enteredPassword);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error password encryption. Error: " + e.Message, "Error- Password Encryption");
            }
            return string.Equals(enteredPasswordHash, HashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
