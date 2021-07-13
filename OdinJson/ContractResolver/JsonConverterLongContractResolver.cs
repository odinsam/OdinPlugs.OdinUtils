using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OdinPlugs.OdinUtils.OdinJson.ContractResolver
{
    public class JsonConverterLongContractResolver : CamelCasePropertyNamesContractResolver
    {
        /// <summary>
        /// 对长整型做处理
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (objectType == typeof(long))
            {
                return new JsonConverterLong();
            }
            return base.ResolveContractConverter(objectType);
        }
    }
}