using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OdinPlugs.OdinUtils.OdinExtensions.BasicExtensions.OdinString;

namespace OdinPlugs.OdinSecurity.StringSecurity.StringSecurityExtensions
{
    public static class StringSecurity
    {
        /// <summary>
        /// string sha256 加密
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>sha256加密后的值</returns>
        public static string Sha256(this string str)
        {
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(str));

            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed Sha256 = new SHA256Managed();
            var by = Sha256.ComputeHash(SHA256Data);
            var result = BitConverter.ToString(by).Replace("-", "").ToLower(); //64
            return result;
        }

        /// <summary>
        /// 微信掩码 
        /// <code>
        /// 掩码规则： AA******  保留前两位，后续全部使用 * 掩码
        /// </code>
        /// </summary>
        /// <param name="entity">用户对象</param>
        /// <param name="fieldName">需要掩码的字段</param>
        /// <param name="newfieldName">掩码后的新字段</param>
        /// <returns>具有掩码字段的对象</returns>
        public static JObject AddWxNameMark(this Object entity, string fieldName, string newfieldName)
        {
            var wxName = entity.GetType().GetProperty(fieldName).GetValue(entity).ToString();
            string wxNameMark = null;
            var star = "";
            for (int i = 0; i < wxName.Length - 2; i++)
            {
                star += "*";
            }
            wxNameMark = wxName.Substring(0, 2) + star;
            var jobj = JObject.Parse(JsonConvert.SerializeObject(entity));
            jobj.Add(newfieldName ?? $"Mark{fieldName}", wxNameMark);
            return jobj;
        }

        /// <summary>
        /// 邮箱掩码
        /// <code>
        /// 掩码规则：如果 @前的邮箱名 length>3 那么 A***B 收尾保留其余用 * 代替， 否则 不做掩码处理
        /// </code>
        /// </summary>
        /// <param name="entity">用户对象</param>
        /// <param name="fieldName">需要掩码的字段</param>
        /// <param name="newfieldName">掩码后的新字段</param>
        /// <returns>具有掩码字段的对象</returns>
        public static JObject AddMailMark(this Object entity, string fieldName, string newfieldName)
        {
            var mail = entity.GetType().GetProperty(fieldName).GetValue(entity).ToString().Split('@')[0].ToString();
            if (mail.IsEmail())
            {
                string mailMark = null;
                if (mail.Length > 3)
                {
                    var star = "";
                    for (int i = 0; i < mail.Length - 2; i++)
                    {
                        star += "*";
                    }
                    mailMark = mail.Substring(0, 1) + star + mail.Substring(mail.Length - 1, 1);
                }
                else
                {
                    mailMark = mail;
                }
                var jobj = JObject.Parse(JsonConvert.SerializeObject(entity));
                jobj.Add(newfieldName ?? $"Mark{fieldName}", mailMark);
                return jobj;
            }
            else
            {
                throw new Exception("邮箱格式不正确");
            }
        }

        /// <summary>
        /// 姓名掩码
        /// <code>
        /// 掩码规则：1. length=2 A*  2. length=3  A*b   3. 其余 A**B，收尾保留其余用 * 代替
        /// </code>
        /// </summary>
        /// <param name="entity">用户对象</param>
        /// <param name="fieldName">需要掩码的字段</param>
        /// <param name="newfieldName">掩码后的新字段</param>
        /// <returns>具有掩码字段的对象</returns>
        public static JObject AddNameMark(this Object entity, string fieldName, string newfieldName)
        {
            var userName = entity.GetType().GetProperty(fieldName).GetValue(entity).ToString();
            string userNameMark = null;
            if (userName.Length == 2)
            {
                userNameMark = userName.Substring(0, 1) + "*";
            }
            else if (userName.Length == 3)
            {
                userNameMark = userName.Substring(0, 1) + "*" + userName.Substring(2, 1);
            }
            else
            {
                var star = "";
                for (int i = 0; i < userName.Length - 2; i++)
                {
                    star += "*";
                }
                userNameMark = userName.Substring(0, 1) + star + userName.Substring(userName.Length - 1, 1);
            }
            var jobj = JObject.Parse(JsonConvert.SerializeObject(entity));
            jobj.Add(newfieldName ?? $"Mark{fieldName}", userNameMark);
            return jobj;
        }

        /// <summary>
        /// 身份证掩码
        /// <code>
        /// 掩码规则：  ABCDEFGHIJ*******K, 获取前10位和最后一位，奇遇用 掩码 代码
        /// </code>
        /// </summary>
        /// <param name="entity">用户对象</param>
        /// <param name="fieldName">需要掩码的字段</param>
        /// <param name="newfieldName">掩码后的新字段</param>
        /// <returns>具有掩码字段的对象</returns>
        public static JObject AddCardIdMark(this Object entity, string fieldName, string newfieldName = null, string strMark = null)
        {
            var cardId = entity.GetType().GetProperty(fieldName).GetValue(entity).ToString();
            if (cardId.IsChinaCardId())
            {
                var cardIdMark = Regex.Replace(cardId, StringRegexExtensions.RegexChinaCardId, strMark ?? StringRegexExtensions.StringMarkChinaCardId);
                var jobj = JObject.Parse(JsonConvert.SerializeObject(entity));
                jobj.Add(newfieldName ?? $"Mark{fieldName}", cardIdMark);
                return jobj;
            }
            else
            {
                throw new Exception("身份证格式不正确");
            }
        }

        /// <summary>
        /// 电话号码掩码
        /// <code>
        /// 掩码规则：  ABC****DEFGHIJ,  前3位 和 后7位，其余用 掩码 代替
        /// </code>
        /// </summary>
        /// <param name="entity">用户对象</param>
        /// <param name="fieldName">需要掩码的字段</param>
        /// <param name="newfieldName">掩码后的新字段</param>
        /// <returns>具有掩码字段的对象</returns>
        public static JObject AddPhoneMark(this Object entity, string fieldName, string newfieldName = null, string strMark = null)
        {
            var phone = entity.GetType().GetProperty(fieldName).GetValue(entity).ToString();
            if (phone.IsChinaMobile())
            {
                var phoneMark = Regex.Replace(phone, StringRegexExtensions.RegexChinaMobile, strMark ?? StringRegexExtensions.StringMarkChinaMobile);
                var jobj = JObject.Parse(JsonConvert.SerializeObject(entity));
                jobj.Add(newfieldName ?? $"Mark{fieldName}", phoneMark);
                return jobj;
            }
            else
            {
                throw new Exception("移动电话号码格式不正确");
            }
        }

        /// <summary>
        /// md5 加密 转小写
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToMd5Lower(this string str, string salt = null, int length = 32)
        {
            MD5 md5 = MD5.Create();
            byte[] bt = md5.ComputeHash(Encoding.UTF8.GetBytes(str + (salt.IsNullOrEmpty() ? "" : salt)));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bt.Length; i++)
            {
                sb.AppendFormat("{0:x2}", bt[i]);
            }
            return sb.ToString().ToLower().Substring(0, length);
        }
        /// <summary>
        /// 两次 md5 加密 转小写
        /// <code>
        /// e.g (str+salt).md5().md5()
        /// </code>
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToMd5Lower2(this string str, string salt = null, int length = 32)
        {
            return str.ToMd5Lower(salt).ToMd5Lower();
        }
        /// <summary>
        /// md5 加密 转大写
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToMd5Upper(this string str, string salt = null, int length = 32)
        {
            MD5 md5 = MD5.Create();
            byte[] bt = md5.ComputeHash(Encoding.UTF8.GetBytes(str + (salt.IsNullOrEmpty() ? "" : salt)));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bt.Length; i++)
            {
                sb.AppendFormat("{0:x2}", bt[i]);
            }
            return sb.ToString().ToUpper().Substring(0, length);
        }
        /// <summary>
        /// 两次 md5 加密 转大写
        /// <code>
        /// e.g (str+salt).md5().md5()
        /// </code>
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToMd5Upper2(this string str, string salt = null, int length = 32)
        {
            return str.ToMd5Upper(salt).ToMd5Upper();
        }
    }
}