using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Education
{
    public sealed class EducationHistory : IEquatable<EducationHistory>
    {
        private readonly List<Education> _educationEntries;

        public IReadOnlyList<Education> EducationEntries => _educationEntries;

        [JsonConstructor]
        private EducationHistory(IReadOnlyList<Education> educationEntries)
        {
            _educationEntries = [.. educationEntries];
        }

        public static EducationHistory Create(IEnumerable<Education> educationEntries)
         => new(educationEntries.ToList());

        public static EducationHistory? From(IEnumerable<Education>? educationEntries)
         => educationEntries is not null ? new(educationEntries.ToList()) : null;

        // Manipulations
        public static EducationHistory Empty() => new([]);

        internal EducationHistory AddEducationEntry(Education educationEntry) =>
            new(_educationEntries.Append(educationEntry).ToList());

        internal EducationHistory RemoveEducationEntry(Education educationEntry) =>
            new(_educationEntries.Where(e => !e.Equals(educationEntry)).ToList());

        internal EducationHistory UpdateEducationEntry(Education oldEntry, Education newEntry) =>
            new(_educationEntries.Select(e => e.Equals(oldEntry) ? newEntry : e).ToList());

        // Equality
        public bool Equals(EducationHistory? other) =>
            other is not null
            && _educationEntries.Count == other._educationEntries.Count
            && _educationEntries.OrderBy(e => e.School.Name)
            .SequenceEqual(other.EducationEntries.OrderBy(o => o.School.Name));

        public override bool Equals(object? obj) => Equals(obj as EducationHistory);

        public override int GetHashCode()
        {
            var hash = new HashCode();

            foreach (var entry in _educationEntries)
            {
                hash.Add(entry);
            }

            return hash.ToHashCode();
        }
    }
}
