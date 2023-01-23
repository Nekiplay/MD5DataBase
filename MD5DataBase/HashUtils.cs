using SHA3.Net;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SHA3DataBase
{
    public class HashUtils
    {
        public static string GetSHA3Hash(FileInfo file)
        {
            using (var shaAlg = Sha3.Sha3256())
            {
                using (var stream = File.OpenRead(file.FullName))
                {
                    var hash = shaAlg.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
        public static string GetSHA3Hash(string text)
        {
            using (var shaAlg = Sha3.Sha3256())
            {
                byte[] hash = shaAlg.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            }
        }
        public static string GetMD5Hash(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(bytes);

            string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            return encoded;
        }
        public static string GetSHA2565Hash(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            using (SHA256 SHA256 = SHA256Managed.Create())
            {
                return Convert.ToBase64String(SHA256.ComputeHash(bytes));
            }
        }
        public static string GetMD5Hash(FileInfo file)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file.FullName))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public static string GetSHA2565Hash(FileInfo filePath)
        {
            using (SHA256 SHA256 = SHA256Managed.Create())
            {
                using (FileStream fileStream = File.OpenRead(filePath.FullName))
                    return Convert.ToBase64String(SHA256.ComputeHash(fileStream));
            }
        }
    }
}
