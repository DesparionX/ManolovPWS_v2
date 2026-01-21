using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Experience
{
    public sealed class JobPeriod : IEquatable<JobPeriod>
    {
        public DateOnly StartDate { get; }
        public DateOnly? EndDate { get; }

        private JobPeriod(DateOnly startDate, DateOnly? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public static JobPeriod Create(DateOnly startDate, DateOnly? endDate)
        {
            ValidateJobPeriod(startDate, endDate);
            return new JobPeriod(startDate, endDate);
        }

        // Validations
        private static void ValidateJobPeriod(DateOnly startDate, DateOnly? endDate)
        {
            if (startDate > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidExperienceException("Start date cannot be in the future.", "InvalidJobStartDate");

            if (endDate.HasValue && endDate < startDate)
                throw new InvalidExperienceException("End date cannot be earlier than start date.", "InvalidJobEndDate");
        }

        // Equality

        public bool Equals(JobPeriod? other) =>
            other is not null
            && StartDate.Equals(other.StartDate)
            && Nullable.Equals(EndDate, other.EndDate);

        public override bool Equals(object? obj) => Equals(obj as JobPeriod);

        public override int GetHashCode() => HashCode.Combine(StartDate, EndDate);

        public override string ToString() =>
            EndDate.HasValue
            ? $"{StartDate:MM/yyyy} - {EndDate:MM/yyyy}"
            : $"{StartDate:MM/yyyy}";
    }
}
