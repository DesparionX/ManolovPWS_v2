using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.Message.Properties.SenderInfo
{
    public sealed class SenderMetadata : IEquatable<SenderMetadata>
    {
        private const int MaxUserAgentLength = 512;

        public string IpAddress { get; }
        public string UserAgent { get; }

        [JsonConstructor]
        private SenderMetadata(string ipAddress, string userAgent)
        {
            IpAddress = ipAddress;
            UserAgent = userAgent;
        }

        public static SenderMetadata Create(string? ipAddress, string? userAgent)
        {
            var ip = string.IsNullOrWhiteSpace(ipAddress) ? "unknown" : ipAddress.Trim();
            var ua = string.IsNullOrWhiteSpace(userAgent)
                ? "unknown"
                : userAgent.Trim()[..Math.Min(userAgent.Trim().Length, MaxUserAgentLength)];

            return new SenderMetadata(ip, ua);
        }

        // Equality
        public bool Equals(SenderMetadata? other) =>
            other is not null
            && IpAddress == other.IpAddress
            && UserAgent == other.UserAgent;

        public override bool Equals(object? obj) => 
            Equals(obj as SenderMetadata);

        public override int GetHashCode() => 
            HashCode.Combine(IpAddress, UserAgent);
    }
}
