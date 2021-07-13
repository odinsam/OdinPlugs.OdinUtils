using Newtonsoft.Json.Serialization;

namespace OdinPlugs.OdinUtils.OdinJson.OdinStrategy
{
    public class NamingStrategyToLower : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            return name.ToLower();
        }
    }
}