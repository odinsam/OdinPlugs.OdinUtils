using System.Collections.Generic;
using System.Threading;
using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Security.Cryptography;
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
        public static JObject AddCardIdMark(this Object entity, string fieldName, string newfieldName = null)
        {
            var cardId = entity.GetType().GetProperty(fieldName).GetValue(entity).ToString();
            var cardIdMark = cardId.Substring(0, 10) + "*******" + cardId.Substring(cardId.Length - 1, 1);
            var jobj = JObject.Parse(JsonConvert.SerializeObject(entity));
            jobj.Add(newfieldName ?? $"Mark{fieldName}", cardIdMark);
            return jobj;
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
        public static JObject AddPhoneMark(this Object entity, string fieldName, string newfieldName = null)
        {
            var phone = entity.GetType().GetProperty(fieldName).GetValue(entity).ToString();
            var phoneMark = phone.Substring(0, 3) + "****" + phone.Substring(7);
            var jobj = JObject.Parse(JsonConvert.SerializeObject(entity));
            jobj.Add(newfieldName ?? $"Mark{fieldName}", phoneMark);
            return jobj;
        }

        /// <summary>
        /// dm5 加密 转小写
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToMd5Lower(this string str, int length = 32)
        {
            MD5 md5 = MD5.Create();
            byte[] bt = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bt.Length; i++)
            {
                sb.AppendFormat("{0:x2}", bt[i]);
            }
            return sb.ToString().ToLower().Substring(0, length);
        }
        /// <summary>
        /// dm5 加密 转大写
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToMd5Upper(this string str, int length = 32)
        {
            MD5 md5 = MD5.Create();
            byte[] bt = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bt.Length; i++)
            {
                sb.AppendFormat("{0:x2}", bt[i]);
            }
            return sb.ToString().ToUpper().Substring(0, length);
        }

    }
}