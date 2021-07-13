using System.Text.Json;

namespace OdinPlugs.OdinUtils.OdinJson.ContractResolver
{
    public class LowerCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) =>
            name.ToLower();
    }
}