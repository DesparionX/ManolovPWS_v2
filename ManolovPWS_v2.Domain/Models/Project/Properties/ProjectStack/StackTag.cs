using ManolovPWS_v2.Domain.Models.Project.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.Project.Properties.ProjectStack
{
    public sealed class StackTag : IEquatable<StackTag>
    {
        public string Tag { get; }

        [JsonConstructor]
        private StackTag(string tag)
        {
            Tag = tag;
        }

        public static StackTag Create(string tag)
        {
            ValidateTag(tag);

            return new StackTag(tag);
        }

        // Validations
        private static void ValidateTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new InvalidProjectStackException("Stack tag cannot be null or empty.");

            if (tag.Length > 20)
                throw new InvalidProjectStackException("Stack tag cannot exceed 20 characters.");
        }

        // Equality
        public bool Equals(StackTag? other) =>
            other is not null
            && StringComparer.InvariantCultureIgnoreCase.Equals(Tag, other.Tag);

        public override bool Equals(object? obj) => Equals(obj as StackTag);

        public override int GetHashCode() => Tag.GetHashCode();

        public override string ToString() => Tag;
    }
}
