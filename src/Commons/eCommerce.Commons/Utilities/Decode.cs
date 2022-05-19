using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Commons.Utilities
{
    public class Decode
    {
        public static string aes(string key, string text)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(text);
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);//I have already defined "Key" in the above EncryptString function
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
            //return Encoding.UTF8.GetString(Convert.FromBase64String(text));
            ////return text;
        }

        public static string Base64(string text)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(text);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
