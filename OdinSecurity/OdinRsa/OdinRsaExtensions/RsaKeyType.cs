using OdinPlugs.OdinUtils.OdinSecurity.OdinRsa.OdinRsaEnum;

namespace OdinPlugs.OdinUtils.OdinSecurity.OdinRsa.OdinRsaExtensions
{
    public class RsaKeyTypeConvert
    {
        public static string PemKeyToString(string key)
        {
            return key.Replace("\r", "").Replace("\n", "").Replace("\r\n", "").Replace(" ", "");
        }
        /// <summary>
        /// 私钥Pem格式转换为xml格式
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="keyType">私钥格式</param>
        /// <returns>xml格式的私钥</returns>
        public static string ConvertPrivateKeyToXml(string privateKey, EnumRsaKeyType? keyType = null)
        {
            if (keyType != null)
            {
                switch (keyType)
                {
                    case EnumRsaKeyType.PKCS1:
                        return PrivateKey_Pkcs1ToXml(privateKey);
                    case EnumRsaKeyType.PKCS8:
                        return PrivateKey_Pkcs8ToXml(privateKey);
                }
            }
            return privateKey;
        }

        /// <summary>
        /// 公钥格式转换 xml->pem
        /// </summary>
        /// <param name="key">公钥</param>
        /// <returns>pem格式的公钥</returns>
        public static string PublicKeyXmlToPem(string key)
        {
            return XC.RSAUtil.RsaKeyConvert.PublicKeyXmlToPem(key);
        }

        /// <summary>
        /// 公钥格式转换 pem->xml
        /// </summary>
        /// <param name="key">公钥</param>
        /// <returns>xml格式的公钥</returns>
        public static string PublicKeyPemToXml(string key)
        {
            return XC.RSAUtil.RsaKeyConvert.PublicKeyPemToXml(key);
        }

        #region  xml->pkcs1
        /// <summary>
        /// 私钥格式转换 xml->pkcs1
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>pkcs1格式私钥</returns>
        public static string PrivateKey_XmlToPkcs1(string privateKey)
        {
            return XC.RSAUtil.RsaKeyConvert.PrivateKeyXmlToPkcs1(privateKey);
        }
        #endregion

        #region xml->pkcs8
        /// <summary>
        /// 私钥格式转换 xml->pkcs8
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>pkcs8格式私钥</returns>
        public static string PrivateKey_XmlToPkcs8(string privateKey)
        {
            return XC.RSAUtil.RsaKeyConvert.PrivateKeyXmlToPkcs8(privateKey);
        }
        #endregion

        #region Pkcs1-> XML
        /// <summary>
        /// 私钥格式转换 Pkcs1-> XML
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>XML格式私钥</returns>
        public static string PrivateKey_Pkcs1ToXml(string privateKey)
        {
            return XC.RSAUtil.RsaKeyConvert.PrivateKeyPkcs1ToXml(privateKey);
        }
        #endregion

        #region pkcs8->xml
        /// <summary>
        /// 私钥格式转换 pkcs8->xml
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>xml格式私钥</returns>
        public static string PrivateKey_Pkcs8ToXml(string privateKey)
        {
            return XC.RSAUtil.RsaKeyConvert.PrivateKeyPkcs8ToXml(privateKey);
        }
        #endregion

        #region Pkcs1-> Pkcs8：
        /// <summary>
        /// 私钥格式转换 Pkcs1-> Pkcs8
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>Pkcs8格式私钥</returns>
        public static string PrivateKey_Pkcs1ToPkcs8(string privateKey)
        {
            return XC.RSAUtil.RsaKeyConvert.PrivateKeyPkcs1ToPkcs8(privateKey);
        }
        #endregion
        #region Pkcs8-> Pkcs1
        /// <summary>
        /// 私钥格式转换 Pkcs8-> Pkcs1
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>Pkcs1格式私钥</returns>
        public static string PrivateKey_Pkcs8ToPkcs1(string privateKey)
        {
            return XC.RSAUtil.RsaKeyConvert.PrivateKeyPkcs8ToPkcs1(privateKey);
        }
        #endregion

    }
}