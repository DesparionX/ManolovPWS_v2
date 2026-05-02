using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.Project.Properties.ProjectStack
{
    public sealed class ProjectStack: IEquatable<ProjectStack>
    {
        private readonly List<StackTag> _stackList;

        public IReadOnlyCollection<StackTag> StackList => _stackList;

        [JsonConstructor]
        private ProjectStack(IEnumerable<StackTag> stackList)
        {
            _stackList = [.. stackList.Distinct()];
        }

        public static ProjectStack Empty() => new([]);

        public static ProjectStack Create(IEnumerable<StackTag>? stackList = default)
        {
            if (stackList is null || !stackList.Any()) return Empty();

            return new(stackList);
        }

        public static ProjectStack? From(ProjectStack? stack)
            => stack is not null ? Create(stack.StackList) : null;

        // Stack manipulations
        internal ProjectStack AddTag(StackTag tag)
            => new(_stackList.Append(tag));

        internal ProjectStack AddTags(IEnumerable<StackTag> tags)
            => new(_stackList.Concat(tags));

        internal ProjectStack RemoveTag(StackTag tag)
            => new(_stackList.Where(t => !t.Equals(tag)));

        internal ProjectStack RemoveTags(IEnumerable<StackTag> tags)
            => new(_stackList.Except(tags));

        // Equality
        public bool Equals(ProjectStack? other) =>
            other is not null
            && _stackList.Count == other._stackList.Count
            && _stackList.OrderBy(s => s.Tag).SequenceEqual(other._stackList.OrderBy(s => s.Tag))
            && !_stackList.Except(other._stackList).Any();

        public override bool Equals(object? obj) => Equals(obj as ProjectStack);

        public override int GetHashCode()
        {
            var hash = new HashCode();
            foreach (var tag in _stackList)
            {
                hash.Add(tag.GetHashCode());
            }
            return hash.ToHashCode();
        }
    }
}
