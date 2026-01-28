using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System.Collections.ObjectModel;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Education
{
    public sealed class EducationHistory : IEquatable<EducationHistory>
    {
        private readonly List<Education> _educationEntries;

        public IReadOnlyCollection<Education> EducationEntries => _educationEntries;

        private EducationHistory(IEnumerable<Education> educationEntries)
        {
            _educationEntries = [.. educationEntries];
        }

        public static EducationHistory Create(IEnumerable<Education> educationEntries)
        {
            ValidateEducationHistory(educationEntries);

            return new EducationHistory(educationEntries);
        }

        // Manipulations
        public static EducationHistory Empty() => new([]);

        public EducationHistory AddEducationEntry(Education educationEntry) =>
            new(_educationEntries.Append(educationEntry));

        public EducationHistory RemoveEducationEntry(Education educationEntry) =>
            new(_educationEntries.Where(e => !e.Equals(educationEntry)));

        public EducationHistory UpdateEducationEntry(Education oldEntry, Education newEntry) =>
            new(_educationEntries.Select(e => e.Equals(oldEntry) ? newEntry : e));

        // Validations
        private static void ValidateEducationHistory(IEnumerable<Education> educationEntries)
        {
            if (educationEntries is null)
                throw new InvalidEducationException("Education entries collection is null.");

            if (!educationEntries.Any())
                throw new InvalidEducationException("Education entries collection is empty.");
        }

        // Equality
        public bool Equals(EducationHistory? other) =>
            other is not null
            && _educationEntries.Count == other._educationEntries.Count
            && _educationEntries.OrderBy(e => e.School.Name)
            .SequenceEqual(other.EducationEntries.OrderBy(o => o.School.Name))
            && !_educationEntries.Except(other.EducationEntries).Any();

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
