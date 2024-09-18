using Newtonsoft.Json;

namespace Apps.EasyTranslate.Utils;

public class NullToDefaultConverter<T> : JsonConverter<T> where T : struct
{
    private readonly T _defaultValue;

    public NullToDefaultConverter(T defaultValue)
    {
        _defaultValue = defaultValue;
    }

    public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }

    public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return _defaultValue;
        }
        return serializer.Deserialize<T>(reader);
    }
}
