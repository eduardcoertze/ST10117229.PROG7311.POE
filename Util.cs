using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ST10117229.PROG7311.POE
{
    public class Util
    {

        //method to covert the plain text code into hash code
        public string hashPassword(string password)
        {

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            byte[] byte_Password = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = sha1.ComputeHash(byte_Password);
            return Convert.ToBase64String(encrypted_bytes);

        }



    }
}