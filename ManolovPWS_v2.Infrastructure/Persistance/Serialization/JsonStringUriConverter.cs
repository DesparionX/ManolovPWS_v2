using System.Text.Json;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Infrastructure.Persistance.Serialization
{
    public sealed class JsonStringUriConverter : JsonConverter<Uri>
    {
        public override Uri Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var uriString = reader.GetString();
            if (string.IsNullOrEmpty(uriString)) return null!;

            return new Uri(uriString, UriKind.RelativeOrAbsolute);
        }

        public override void Write(Utf8JsonWriter writer, Uri value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
