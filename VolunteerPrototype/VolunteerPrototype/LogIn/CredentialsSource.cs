using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace VolunteerPrototype.LogIn
{
    static class CredentialsSource
    {
        static System.Collections.Hashtable credentials;
        static CredentialsSource()
        {
            credentials = new System.Collections.Hashtable();
            credentials.Add("Guest", GetHash(""));
            credentials.Add("John", GetHash("qwerty"));
            credentials.Add("Administrator", GetHash("admin"));
            credentials.Add("Mary", GetHash("12345"));
        }
        internal static bool Check(string login, string pwd)
        {
            return object.Equals(credentials[login], GetHash(pwd));
        }
        static object GetHash(string password)
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
        internal static System.Collections.Generic.IEnumerable<string> GetUserNames()
        {
            foreach (string item in credentials.Keys)
                yield return item;
        }
    }
}
