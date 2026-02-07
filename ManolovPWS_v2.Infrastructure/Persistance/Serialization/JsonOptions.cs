using System.Text.Json;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Infrastructure.Persistance.Serialization
{
    internal static class JsonOptions
    {
        internal static readonly JsonSerializerOptions Default = new()
        {
            WriteIndented = false,
            IncludeFields = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter(),
                new JsonStringUriConverter(),
            }
        };
    }
}
