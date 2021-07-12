using System.Text.Json;

namespace OdinPlugs.OdinUtils.OdinJson.ContractResolver
{
    public class ToUpperCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) =>
            name.ToUpper();
    }
}