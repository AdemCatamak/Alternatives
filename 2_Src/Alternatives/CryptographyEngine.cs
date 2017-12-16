using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Alternatives
{
    public interface ICryptoEngine
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
        string Hashing(string plainText);
    }


    public class CryptographyEngine : ICryptoEngine
    {
        private const int KEYSIZE = 256;
        private string _keyCode = "N+qg88QJ)2r%]_u";

        private readonly byte[] _initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");

        public CryptographyEngine(string keyCode = null)
        {
            if (keyCode != null)
            {
                _keyCode = keyCode;
            }
        }

        public void SetKeyCode(string keyCode)
        {
            if (keyCode != null)
            {
                _keyCode = keyCode;
            }
        }

        public string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(_keyCode, null))
            {
#pragma warning disable 618
                byte[] keyBytes = password.GetBytes(KEYSIZE / 8);
#pragma warning restore 618
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, _initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (
                                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor,
                                                                             CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] cipherTextBytes = memoryStream.ToArray();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(_keyCode, null))
            {
#pragma warning disable 618
                byte[] keyBytes = password.GetBytes(KEYSIZE / 8);
#pragma warning restore 618
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, _initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (
                                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor,
                                                                             CryptoStreamMode.Read))
                            {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        public string Hashing(string plainText)
        {
            using (SHA1 hash = SHA1.Create())
            {
                return BitConverter.ToString(hash.ComputeHash(Encoding.ASCII.GetBytes(plainText)));
            }
        }
    }
}