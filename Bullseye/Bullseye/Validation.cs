using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public static bool VerifyPassword(string enteredPassword, string HashedPassword)
        {
            string enteredPasswordHash = HashPassword(enteredPassword);      
            return string.Equals(enteredPasswordHash, HashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
