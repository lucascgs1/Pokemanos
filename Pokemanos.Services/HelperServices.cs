using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Pokemanos.Services
{
    public static class HelperServices
    {
        public static string Md5Hash(this string text)
        {
            using MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
