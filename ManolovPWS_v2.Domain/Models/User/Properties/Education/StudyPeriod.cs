using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Education
{
    public sealed class StudyPeriod : IEquatable<StudyPeriod>
    {
        public DateOnly StartDate { get; }
        public DateOnly? EndDate { get; }

        [JsonConstructor]
        private StudyPeriod(DateOnly startDate, DateOnly? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public static StudyPeriod Create(DateOnly startDate, DateOnly? endDate)
        {
            ValidateStudyPeriod(startDate, endDate);
            return new StudyPeriod(startDate, endDate);
        }

        // Validations
        private static void ValidateStudyPeriod(DateOnly startDate, DateOnly? endDate)
        {
            if (startDate > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidEducationException("Start date cannot be in the future.", "InvalidStudyStartDate");

            if (endDate is not null && endDate < startDate)
                throw new InvalidEducationException("End date cannot be earlier than start date.", "InvalidStudyEndDate");
        }

        // Equality
        public bool Equals(StudyPeriod? other) =>
            other is not null
            && StartDate == other.StartDate
            && EndDate == other.EndDate;

        public override bool Equals(object? obj) => Equals(obj as StudyPeriod);

        public override int GetHashCode() => HashCode.Combine(StartDate, EndDate);

        public override string ToString() =>
            EndDate is not null
            ? $"{StartDate:yyyy-MM} - {EndDate:yyyy-MM}"
            : $"{StartDate:yyyy-MM}";
    }
}