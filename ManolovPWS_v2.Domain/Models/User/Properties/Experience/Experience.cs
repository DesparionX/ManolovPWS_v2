using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Experience
{
    public sealed class Experience : IEquatable<Experience>
    {
        private readonly ReadOnlyCollection<Job> _jobs;

        public IReadOnlyList<Job> Jobs => _jobs;

        private Experience(IEnumerable<Job> jobs)
        {
            _jobs = jobs.ToList().AsReadOnly();
        }

        public static Experience Create(IEnumerable<Job> jobs)
        {
            ValidateExperience(jobs);

            return new Experience(jobs);
        }

        // Manipulations
        public static Experience Empty() => new([]);

        public Experience AddJob(Job job)
            => new(_jobs.Append(job));

        public Experience RemoveJob(Job job)
            => new(_jobs.Where(j => !j.Equals(job)));

        public Experience UpdateJob(Job oldJob, Job newJob)
            => new(_jobs.Select(j => j.Equals(oldJob) ? newJob : j));

        // Validations
        private static void ValidateExperience(IEnumerable<Job> jobs)
        {
            if (jobs is null)
                throw new InvalidExperienceException("Jobs collection is null.");

            if (!jobs.Any())
                throw new InvalidExperienceException("Jobs collection is empty.");
        }

        // Equality
        public bool Equals(Experience? other) =>
            other is not null
            && _jobs.SequenceEqual(other._jobs);

        public override bool Equals(object? obj) => Equals(obj as Experience);

        public override int GetHashCode() => HashCode.Combine(_jobs);

        public override string ToString() => string.Join(", ", _jobs.Select(j => j.Title));
        
    }
}
