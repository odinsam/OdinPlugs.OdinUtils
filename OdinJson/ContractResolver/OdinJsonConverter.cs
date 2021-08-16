using Newtonsoft.Json.Serialization;

namespace OdinPlugs.OdinUtils.OdinJson.ContractResolver
{
    public class OdinJsonConverter
    {
        public static IContractResolver SetOdinJsonConverter(enumOdinJsonConverter enumCase)
        {
            return enumCase switch
            {
                enumOdinJsonConverter.CamelCase => new CamelCasePropertyNamesContractResolver(),
                enumOdinJsonConverter.LowerCase => new ToLowerPropertyNamesContractResolver(),
                _ => new Newtonsoft.Json.Serialization.DefaultContractResolver()
            };
        }
    }
}