using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Apps.EasyTranslate.Utils;

public class DictionaryOrArrayConverter<TKey, TValue> : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Dictionary<TKey, TValue>);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var token = JToken.Load(reader);

        if (token.Type == JTokenType.Array)
        {
            return new Dictionary<TKey, TValue>();
        }

        return token.ToObject<Dictionary<TKey, TValue>>();
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}