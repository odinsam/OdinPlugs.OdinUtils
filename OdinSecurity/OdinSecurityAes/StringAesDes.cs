using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using OdinPlugs.OdinUtils.OdinSecurity.OdinEncodeConvert;

namespace OdinPlugs.OdinUtils.OdinSecurity.OdinSecurityAes
{
    public class StringAesDes
    {
        /// <summary>  
        /// AES加密算法  
        /// </summary>  
        /// <param name="input">明文字符串</param>  
        /// <param name="key">密钥（32位）</param>  
        /// <param name="aes_ai">加密偏移量</param>  
        /// <returns>字符串</returns>  
        public static string EncryptByAES(string input, string key, string aes_ai = null)
        {
            if (key.Length != 32)
                throw new Exception("密钥需要是32位的字符串");
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 32));
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = keyBytes;

                ICryptoTransform encryptor = null;
                if (!string.IsNullOrEmpty(aes_ai))
                {
                    aesAlg.IV = Encoding.UTF8.GetBytes(aes_ai.Substring(0, 16));
                    encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                }
                else
                {
                    encryptor = aesAlg.CreateEncryptor(aesAlg.Key, null);
                }
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                        byte[] bytes = msEncrypt.ToArray();
                        return StringEncodeConvert.ByteArrayToHexString(bytes);
                    }
                }
            }
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="input">密文字节数组</param>  
        /// <param name="key">密钥（32位）</param>  
        /// <param name="aes_ai">加密偏移量</param>  
        /// <returns>返回解密后的字符串</returns>  
        public static string DecryptByAES(string input, string key, string aes_ai = null)
        {
            if (key.Length != 32)
                throw new Exception("密钥需要是32位的字符串");
            byte[] inputBytes = StringEncodeConvert.HexStringToByteArray(input);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 32));
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = keyBytes;
                ICryptoTransform decryptor = null;
                if (!string.IsNullOrEmpty(aes_ai))
                {
                    aesAlg.IV = Encoding.UTF8.GetBytes(aes_ai.Substring(0, 16));
                    decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                }
                else
                {
                    decryptor = aesAlg.CreateDecryptor(aesAlg.Key, null);
                }
                using (MemoryStream msEncrypt = new MemoryStream(inputBytes))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srEncrypt = new StreamReader(csEncrypt))
                        {
                            return srEncrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}