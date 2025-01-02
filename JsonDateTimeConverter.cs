using Newtonsoft.Json;
using System;

namespace HotelChatbot.Converters
{
    public class JsonDateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is DateTime dateTime)
            {
                writer.WriteValue(dateTime.ToString("yyyy-MM-ddTHH:mm:ss"));
            }
            else
            {
                writer.WriteNull();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Date)
            {
                return reader.Value is DateTime dateTime ? dateTime : DateTime.MinValue;
            }

            return DateTime.MinValue;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
    }
}
