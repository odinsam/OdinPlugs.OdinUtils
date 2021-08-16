using System;
using Newtonsoft.Json;

namespace OdinPlugs.OdinUtils.OdinJson.ContractResolver.DateTimeContractResolver
{
    public class DateTimeNullableConverter : JsonConverter<DateTime?>
    {
        public string DateTimeType { get; set; }
        public DateTimeNullableConverter(string dateTimeType)
        {
            this.DateTimeType = dateTimeType;
        }
        public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return string.IsNullOrEmpty(reader.ToString()) ? default(DateTime?) : DateTime.Parse(reader.ToString());
        }

        public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString(DateTimeType));
        }
    }
}