using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    public static class Utility
    {
        public static string MD5Encrypt(string encryptedStr)
        {
            byte[] bytes = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(encryptedStr));
            StringBuilder sb = new StringBuilder();

            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
