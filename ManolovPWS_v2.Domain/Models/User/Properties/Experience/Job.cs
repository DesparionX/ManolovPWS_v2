using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Experience
{
    public sealed class Job : IEquatable<Job>
    {
        public JobTitle Title { get; }
        public CompanyName Company { get; }
        public JobDescription Description { get; }
        public JobPeriod Period { get; }

        [JsonConstructor]
        private Job(JobTitle title, CompanyName company, JobDescription description, JobPeriod period)
        {
            Title = title;
            Company = company;
            Description = description;
            Period = period;
        }

        public static Job Create(JobTitle title, CompanyName company, JobDescription description, JobPeriod period)
        {
            ValidateJob(title, company, description, period);

            return new Job(title, company, description, period);
        }

        // Validations
        private static void ValidateJob(JobTitle title, CompanyName company, JobDescription description, JobPeriod period)
        {
            if (title is null)
                throw new InvalidExperienceException("Title is null.");

            if (company is null)
                throw new InvalidExperienceException("Company is null.");

            if (description is null)
                throw new InvalidExperienceException("Description is null.");

            if (period is null)
                throw new InvalidExperienceException("Period is null.");
        }

        // Equality
        public bool Equals(Job? other) =>
            other is not null
            && Title.Equals(other.Title)
            && Company.Equals(other.Company)
            && Description.Equals(other.Description)
            && Period.Equals(other.Period);

        public override bool Equals(object? obj) => Equals(obj as Job);

        public override int GetHashCode() => HashCode.Combine(Title, Company, Description, Period);

        public override string ToString() => Title.ToString();
    }
}
