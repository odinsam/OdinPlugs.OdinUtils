using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace OdinPlugs.OdinUtils.OdinExtensions.BasicExtensions.OdinNum.OdinHttp
{
    public static class HttpRequestExtends
    {
        /// <summary>
        /// 获取 QueryString
        /// </summary>
        /// <param name="requestUri">请求的uri</param>
        /// <returns>QueryString key-value 形式</returns>
        public static Dictionary<string, string> GetRequestQueryString(this string requestUri)
        {
            string queryString = null;
            if (requestUri.IndexOf('?') >= 0)
                queryString = requestUri.Split('?')[1];
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in queryString.Split('&'))
            {
                dic.Add(item.Split('=')[0], item.Split('=')[1]);
            }
            return dic;
        }
        public static string ReadRequestBody(this HttpRequest request)
        {
            var reader = new StreamReader(request.Body);
            var data = reader.ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return data.Result;
        }
    }
}