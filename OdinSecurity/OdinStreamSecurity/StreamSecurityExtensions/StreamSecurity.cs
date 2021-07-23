using System;
using System.IO;
using System.Security.Cryptography;
namespace OdinPlugs.OdinUtils.OdinSecurity.OdinStreamSecurity.StreamSecurityExtensions
{
    public static class StreamSecurity
    {
        /// <summary>
        /// stream sha256 加密
        /// </summary>
        /// <param name="stream">需要加密的流</param>
        /// <returns>sha256加密后的string信息</returns>
        public static string Sha256(this Stream stream)
        {
            SHA256Managed Sha256 = new SHA256Managed();
            var by = Sha256.ComputeHash(stream);
            var result = BitConverter.ToString(by).Replace("-", "").ToLower(); //64
            return result;
        }
    }
}