using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace BitsOrchestraTest.Core.Helpers
{
    public static class DataProtectionHelper
    {
        private static byte[] IV = Encoding.ASCII.GetBytes(@"9/\~V).A,lY&=t2b");
        private static byte[] Key = Encoding.ASCII.GetBytes(@"qwr{@^h`h&_`50/ja9!'dcmh3!uw<&=?");

        public static string EncryptId(Guid id)
        {
            var plainText = id.ToString();
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException(nameof(IV));

            byte[] encrypted;

            using (var aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }

        public static Guid DecryptId(string encryptedId)
        {
            if (encryptedId == null || encryptedId.Length <= 0)
                throw new ArgumentNullException(nameof(encryptedId));
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException(nameof(Key));
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException(nameof(IV));

            string plaintext = null;
            byte[] cipherText = Convert.FromBase64String(encryptedId.Replace(' ', '+'));

            using (var aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;


                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return Guid.Parse(plaintext);
        }
    }
}
