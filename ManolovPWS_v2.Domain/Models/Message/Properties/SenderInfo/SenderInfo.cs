using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.Message.Properties.SenderInfo
{
    public sealed class SenderInfo : IEquatable<SenderInfo>
    {
        public SenderMetadata Metadata { get; }
        public SenderUsername Username { get; }
        public SenderEmail Email { get; }

        [JsonConstructor]
        private SenderInfo(SenderMetadata metadata, SenderUsername username, SenderEmail email)
        {
            Metadata = metadata;
            Username = username;
            Email = email;
        }

        public static SenderInfo Create(SenderMetadata metadata, SenderUsername username, SenderEmail email)
            => new (metadata, username, email);

        // Equality
        public bool Equals(SenderInfo? other) =>
            other is not null &&
            Metadata.Equals(other.Metadata) &&
            Username.Equals(other.Username) &&
            Email.Equals(other.Email);

        public override bool Equals(object? obj) => 
            Equals(obj as SenderInfo);

        public override int GetHashCode() => 
            HashCode.Combine(Metadata, Username, Email);
    }
}
