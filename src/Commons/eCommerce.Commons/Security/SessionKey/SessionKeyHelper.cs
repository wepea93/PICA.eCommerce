using System.Security.Cryptography;
using System.Text;

namespace eCommerce.Commons.Security.SessionKey
{
    public class SessionKeyHelper : ISessionKeyHelper
    {
        private const int _keysize = 256;
        private const string _token = "A82pgia43P8h2JMdRfQxVLvHDMp5K4GB1f5nXwghfiOLJ4ncuX";
        private const int _derivationIterations = 1000;
        private string _sessionKeyTokenFormat { get; } = "[USER_ID]|[SESSION_KEY]"; 

        public string GenerateSessionKeyToken(string userId, string sessionKey) 
        {
            var token = _sessionKeyTokenFormat.Replace("[USER_ID]", userId);
            token = token.Replace("[SESSION_KEY]", sessionKey);
            return Encrypt(token);
        }
        public string GetSessionKey(string sessionKeyToken)
        {
            var token = Decrypt(sessionKeyToken);
            var items = token.Split('|');
            return items[1];
        }
        public string GetUserId(string sessionKeyToken)
        {
            var token = Decrypt(sessionKeyToken);
            var items = token.Split('|');
            return items[0];
        }

        private static string Encrypt(string plainText)
        {
            byte[] salt = Generate256BitsOfRandomEntropy();
            byte[] rgbIV = Generate256BitsOfRandomEntropy();
            byte[] bytes1 = Encoding.UTF8.GetBytes(plainText);
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(_token, salt, _derivationIterations))
            {
                byte[] bytes2 = rfc2898DeriveBytes.GetBytes(32);
                using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
                {
                    rijndaelManaged.BlockSize = _keysize;
                    rijndaelManaged.Mode = CipherMode.CBC;
                    rijndaelManaged.Padding = PaddingMode.PKCS7;
                    using (ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(bytes2, rgbIV))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(bytes1, 0, bytes1.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] array = ((IEnumerable<byte>)((IEnumerable<byte>)salt).Concat<byte>((IEnumerable<byte>)rgbIV).ToArray<byte>()).Concat<byte>((IEnumerable<byte>)memoryStream.ToArray()).ToArray<byte>();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(array);
                            }
                        }
                    }
                }
            }
        }

        private static string Decrypt(string cipherText)
        {
            byte[] numArray1 = Convert.FromBase64String(cipherText);
            byte[] array1 = ((IEnumerable<byte>)numArray1).Take<byte>(32).ToArray<byte>();
            byte[] array2 = ((IEnumerable<byte>)numArray1).Skip<byte>(32).Take<byte>(32).ToArray<byte>();
            byte[] array3 = ((IEnumerable<byte>)numArray1).Skip<byte>(64).Take<byte>(numArray1.Length - 64).ToArray<byte>();
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(_token, array1, _derivationIterations))
            {
                byte[] bytes = rfc2898DeriveBytes.GetBytes(32);
                using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
                {
                    rijndaelManaged.BlockSize = _keysize;//256
                    rijndaelManaged.Mode = CipherMode.CBC;
                    rijndaelManaged.Padding = PaddingMode.PKCS7;
                    using (ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(bytes, array2))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(array3))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] numArray2 = new byte[array3.Length];
                                int count = cryptoStream.Read(numArray2, 0, numArray2.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(numArray2, 0, count);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            byte[] data = new byte[32];
            using (RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider())
                cryptoServiceProvider.GetBytes(data);
            return data;
        }
    }
}
