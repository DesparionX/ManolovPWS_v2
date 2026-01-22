using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Education
{
    public sealed class Education : IEquatable<Education>
    {
        public School School { get; }
        public Degree Degree { get; }
        public FieldOfStudy FieldOfStudy { get; }
        public StudyPeriod StudyPeriod { get; }

        private Education(
            School school,
            Degree degree,
            FieldOfStudy fieldOfStudy,
            StudyPeriod studyPeriod)
        {
            School = school;
            Degree = degree;
            FieldOfStudy = fieldOfStudy;
            StudyPeriod = studyPeriod;
        }

        public static Education Create(
            School school,
            Degree degree,
            FieldOfStudy fieldOfStudy,
            StudyPeriod studyPeriod) =>
            new(school, degree, fieldOfStudy, studyPeriod);

        // Equality
        public bool Equals(Education? other) =>
            other is not null
            && School.Equals(other.School)
            && Degree.Equals(other.Degree)
            && FieldOfStudy.Equals(other.FieldOfStudy);

        public override bool Equals(object? obj) => Equals(obj as Education);

        public override int GetHashCode() => HashCode.Combine(School, Degree, FieldOfStudy);

    }
}
