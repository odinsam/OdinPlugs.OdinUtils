using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OdinPlugs.OdinUtils.OdinExtensions.BasicExtensions.OdinString
{
    public static class StringExtensions
    {

        /// <summary>
        /// Indicates whether the specified string is null or an empty string ("").
        /// </summary>
        /// <param name="str">The string to test</param>
        /// <returns>true if the value parameter is null or an empty string (""); otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        /// <summary>  
        /// Unicode字符串转为正常字符串  
        /// </summary>  
        /// <param name="srcText"></param>  
        /// <returns></returns>  
        public static string UnicodeToString(this string srcText)
        {
            return Regex.Unescape(srcText);
        }
        /// <summary>
        /// string to ascii
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>ascii code</returns>
        public static string ToAscii(this string str)
        {
            byte[] array = System.Text.Encoding.ASCII.GetBytes(str);
            string ASCIIstr2 = null;
            for (int i = 0; i < array.Length; i++)
            {
                int asciicode = (int)(array[i]);
                ASCIIstr2 += Convert.ToString(asciicode);
            }
            return ASCIIstr2;
        }


        /// <summary>
        /// StringToBase64String
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>Base64String</returns>
        public static string StringToBase64String(this string str)
        {
            byte[] bt = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bt);
        }

        /// <summary>
        /// Base64StringToString
        /// </summary>
        /// <param name="str">Base64String</param>
        /// <returns>String</returns>
        public static string Base64StringToString(this string str)
        {
            byte[] bt = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(bt);
        }

        /// <summary>
        /// 格式化json字符串
        /// </summary>
        /// <param name="str">json string</param>
        /// <returns>json format string</returns>
        public static string ToJsonFormatString(this string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }

        private static XmlDocument GetXmlDocument(string xmlString)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlString);
            return document;
        }

        private static string ConvertXmlDocumentTostring(XmlDocument xmlDocument)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(memoryStream, null)
            {
                Formatting = System.Xml.Formatting.Indented //缩进
            };
            xmlDocument.Save(writer);
            StreamReader streamReader = new StreamReader(memoryStream);
            memoryStream.Position = 0;
            string xmlString = streamReader.ReadToEnd();
            streamReader.Close();
            memoryStream.Close();
            return xmlString;
        }

        /// <summary>
        /// to xml string
        /// </summary>
        /// <param name="XMLstring">xml string</param>
        /// <returns>xml format string</returns>
        public static string ToXmlFormatString(this string XMLstring)
        {
            XmlDocument xmlDocument = GetXmlDocument(XMLstring);
            return ConvertXmlDocumentTostring(xmlDocument);
        }

        /// <summary>
        /// string转16进制
        /// </summary>
        /// <param name="strASCII">string to ascii</param>
        /// <param name="separator">分隔符</param>
        /// <returns>16进制的string</returns>
        public static string ConvertStringToHex(this string strASCII, string separator = null)
        {
            StringBuilder sbHex = new StringBuilder();
            foreach (char chr in strASCII)
            {
                sbHex.Append(String.Format("{0:X2}", Convert.ToInt32(chr)));
                sbHex.Append(separator ?? string.Empty);
            }
            return sbHex.ToString();
        }

        /// <summary>
        /// 16进制转string
        /// </summary>
        /// <param name="HexValue">16进制string</param>
        /// <param name="separator">分隔符</param>
        /// <returns>string说</returns>
        public static string ConvertHexToString(this string HexValue, string separator = null)
        {
            HexValue = string.IsNullOrEmpty(separator) ? HexValue : HexValue.Replace(string.Empty, separator);
            StringBuilder sbStrValue = new StringBuilder();
            while (HexValue.Length > 0)
            {
                sbStrValue.Append(Convert.ToChar(Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString());
                HexValue = HexValue.Substring(2);
            }
            return sbStrValue.ToString();
        }

        /// <summary>
        /// string to bytes
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>byte array</returns>
        public static byte[] GetBytesFromString(this string str)
        {
            return System.Text.Encoding.Default.GetBytes(str);
        }

        /// <summary>
        /// bytes to string
        /// </summary>
        /// <param name="bytes">byte array</param>
        /// <returns>string</returns>
        public static string GetStringFromBytes(this byte[] bytes)
        {
            return System.Text.Encoding.Default.GetString(bytes);
        }

        /// <summary>
        /// Utf8ToGb2312
        /// </summary>
        /// <param name="text">string</param>
        /// <returns>Gb2312 string</returns>
        public static string Utf8ToGb2312(this string text)
        {
            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            byte[] utf;
            utf = utf8.GetBytes(text);
            utf = System.Text.Encoding.Convert(utf8, gb2312, utf);
            //返回转换后的字符   
            return gb2312.GetString(utf);
        }

        /// <summary>
        /// StringEncode
        /// </summary>
        /// <param name="text">string</param>
        /// <param name="sourceEncode">source encode</param>
        /// <param name="transEncode">trans Encode</param>
        /// <returns>string</returns>
        public static string ConvertStringEncode(this string text, Encoding sourceEncode, Encoding transEncode)
        {
            byte[] utf;
            utf = sourceEncode.GetBytes(text);
            utf = System.Text.Encoding.Convert(sourceEncode, transEncode, utf);
            //返回转换后的字符   
            return transEncode.GetString(utf);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sourceEncodeStr"></param>
        /// <param name="transEncodeStr"></param>
        /// <returns></returns>
        public static string ConvertStringEncode(this string text, string sourceEncodeStr, string transEncodeStr)
        {
            Encoding sourceEncode = Encoding.GetEncoding(sourceEncodeStr);
            Encoding transEncode = Encoding.GetEncoding(transEncodeStr);
            byte[] utf;
            utf = sourceEncode.GetBytes(text);
            utf = System.Text.Encoding.Convert(sourceEncode, transEncode, utf);
            //返回转换后的字符   
            return transEncode.GetString(utf);
        }

        public static T ToEnum<T>(this string text)
        {
            return (T)Enum.Parse(typeof(T), text);
        }

        public static string QueryStringToJson(this string queryString)
        {
            List<string> strlst = queryString.Split('&').ToList();
            JObject jsonObject = new JObject();
            foreach (var item in strlst)
            {
                string key = item.Split('=')[0];
                string value = item.Split('=')[1];
                jsonObject.Add(key, value);
            }
            return jsonObject.ToString();

        }

        public static DateTime ConvertStringToDateTime(this string dateTimeStr, string dateTimeFormat)
        {
            DateTime dt = DateTime.ParseExact(dateTimeStr, dateTimeFormat, System.Globalization.CultureInfo.CurrentCulture);
            return dt;
        }


    }
}