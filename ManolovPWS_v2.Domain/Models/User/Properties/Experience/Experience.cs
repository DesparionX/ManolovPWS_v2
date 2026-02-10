using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Experience
{
    public sealed class Experience : IEquatable<Experience>
    {
        private readonly List<Job> _jobs;

        public IReadOnlyCollection<Job> Jobs => _jobs;

        [JsonConstructor]
        private Experience(IEnumerable<Job> jobs)
        {
            _jobs = [.. jobs];
        }

        public static Experience Create(IEnumerable<Job> jobs)
            => new(jobs);

        public static Experience? From(IEnumerable<Job>? jobs)
            => jobs is not null ? new(jobs) : null;

        // Manipulations
        public static Experience Empty() => new([]);

        internal Experience AddJob(Job job)
            => new(_jobs.Append(job));

        internal Experience RemoveJob(Job job)
            => new(_jobs.Where(j => !j.Equals(job)));

        internal Experience UpdateJob(Job oldJob, Job newJob)
            => new(_jobs.Select(j => j.Equals(oldJob) ? newJob : j));

        // Equality
        public bool Equals(Experience? other) =>
            other is not null
            && _jobs.Count == other._jobs.Count
            && !_jobs.Except(other._jobs).Any()
            && _jobs.OrderBy(j => j.Title).SequenceEqual(other._jobs.OrderBy(j => j.Title));

        public override bool Equals(object? obj) => Equals(obj as Experience);

        public override int GetHashCode()
        {
            var hash = new HashCode();

            foreach (var job in _jobs)
            {
                hash.Add(job);
            }

            return hash.ToHashCode();
        }

        public override string ToString() => string.Join(", ", _jobs.Select(j => j.Title));
        
    }
}
