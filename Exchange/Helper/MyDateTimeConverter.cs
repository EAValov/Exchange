using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Helper
{
    public class MyDateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTime.Parse(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //my datetime format
            writer.WriteValue(((DateTime)value).ToString("dd/MM/yyyy HH:mm:ss"));
        }
    }
}