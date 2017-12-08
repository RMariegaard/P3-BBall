using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace VolunteerSystem
{
    public class AccountController
    {
        public static void Login()
        {
            throw new NotImplementedException();
        }

        public static void CreateAccount()
        {

            throw new NotImplementedException();
        }

        public static void ForgotPassword()
        {
            throw new NotImplementedException();
        }

        public static object GetHash(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            var hashAlg = SHA256Managed.Create();
            byte[] hash = hashAlg.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
