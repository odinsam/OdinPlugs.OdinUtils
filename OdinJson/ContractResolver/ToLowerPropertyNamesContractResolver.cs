using Newtonsoft.Json.Serialization;
using OdinPlugs.OdinUtils.OdinJson.OdinStrategy;

namespace OdinPlugs.OdinUtils.OdinJson.ContractResolver
{
    public class ToLowerPropertyNamesContractResolver : DefaultContractResolver
    {
        public ToLowerPropertyNamesContractResolver()
        {
            base.NamingStrategy = new NamingStrategyToLower();
        }
    }
}