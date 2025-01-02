using Newtonsoft.Json;

using System;

namespace HotelChatbot.Converters
{
    public class JsonDateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime dateTime)
            {
                // Write the DateTime as a string in the format "yyyy-MM-ddTHH:mm:ss"
                writer.WriteValue(dateTime.ToString("yyyy-MM-ddTHH:mm:ss"));
            }
            else
            {
                writer.WriteNull();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Date)
            {
                return (DateTime)reader.Value;
            }

            return DateTime.MinValue;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
    }
}
