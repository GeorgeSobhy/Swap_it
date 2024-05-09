using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace _97Display.Mapper.Models.Infrastructure
{
    public class EncryptionHelper
    {
        private static byte[] Salt;
        private static string SharedSecret;

        static EncryptionHelper()
        {
            Salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");
            SharedSecret = "123456";
        }

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException("plainText");
            }

            if (string.IsNullOrEmpty(SharedSecret))
            {
                throw new ArgumentNullException("sharedSecret");
            }

            string outStr = null;
            // Encrypted string to return
            RijndaelManaged aesAlg = null;
            // RijndaelManaged object used to encrypt the data.
            try
            {
                // generate the key from the shared secret and the salt
                var key = new Rfc2898DeriveBytes(SharedSecret, Salt);

                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes((int)Math.Round(aesAlg.KeySize / 8d));
                aesAlg.IV = key.GetBytes((int)Math.Round(aesAlg.BlockSize / 8d));

                // Create a decrytor to perform the stream transform.
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {

                            // Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                    }

                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg is object)
                {
                    aesAlg.Clear();
                }
            }

            // Return the encrypted bytes from the memory stream.
            return outStr;
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                throw new ArgumentNullException("cipherText");
            }

            if (string.IsNullOrEmpty(SharedSecret))
            {
                throw new ArgumentNullException("sharedSecret");
            }

            cipherText = cipherText.Replace(" ", "+");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;
            try
            {
                // generate the key from the shared secret and the salt
                var key = new Rfc2898DeriveBytes(SharedSecret, Salt);

                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes((int)Math.Round(aesAlg.KeySize / 8d));
                aesAlg.IV = key.GetBytes((int)Math.Round(aesAlg.BlockSize / 8d));

                // Create a decrytor to perform the stream transform.
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for decryption.                
                var bytes = Convert.FromBase64String(cipherText);
                using (var msDecrypt = new MemoryStream(bytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            finally
            {
                if (aesAlg is object)
                {
                    aesAlg.Clear();
                }
            }

            return plaintext;
        }
    }
}