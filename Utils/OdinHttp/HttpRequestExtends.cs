using System.IO;
using Microsoft.AspNetCore.Http;

namespace OdinPlugs.OdinUtils.Utils.OdinHttp
{
    public static class HttpRequestExtends
    {
        public static string ReadRequestBody(this HttpRequest request)
        {
            var reader = new StreamReader(request.Body);
            var data = reader.ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return data.Result;
        }
    }
}