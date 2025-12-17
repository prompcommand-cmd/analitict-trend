using Infrastructure.Abstracts;
using Infrastructure.Models;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Utilities
{
    public class Cryptography : ICryptography
    {
        private readonly CryptographySetting _cryptographySettings;
        public Cryptography(CryptographySetting cryptographySettings)
        {
            _cryptographySettings = cryptographySettings;
        }
        public string AesDecrypt(string text)
        {
            // Check if text is null or empty.
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }
            // Use the default key.
            var keys = Encoding.UTF8.GetBytes(_cryptographySettings.Key);
            try
            {

                // Replace space to plus sign.
                text = text.Replace(" ", "+");
                // Convert it into array byte.
                var fullCipher = Convert.FromBase64String(text);

                var iv = new byte[16];
                var cipher = new byte[fullCipher.Length - iv.Length];

                // Fill the iv array.
                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                // Fill the chiper array.
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);

                // Create the AES variable.
                using var aesAlg = Aes.Create();
                // Create decryptor based on keys and IV.
                using var decryptor = aesAlg.CreateDecryptor(keys, iv);
                string result;
                // Decyper the string.
                using (var msDecrypt = new MemoryStream(cipher))
                {
                    using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                    using var srDecrypt = new StreamReader(csDecrypt);
                    result = srDecrypt.ReadToEnd();
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
