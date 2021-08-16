using System;
using System.Text.Json;
using Newtonsoft.Json;

namespace OdinPlugs.OdinUtils.OdinJson.ContractResolver.DateTimeContractResolver
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public string DateTimeType { get; set; }
        public DateTimeConverter(string dateTimeType)
        {
            this.DateTimeType = dateTimeType;
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            return DateTime.Parse(reader.ToString());
        }

        public override void WriteJson(JsonWriter writer, DateTime value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(DateTimeType));
        }
    }
}