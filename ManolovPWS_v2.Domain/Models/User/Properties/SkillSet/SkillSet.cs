using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.SkillSet
{
    public sealed class SkillSet : IEquatable<SkillSet>
    {
        private readonly List<Skill> _skills;
        private readonly List<LanguageSkill> _languages;

        public IReadOnlyList<Skill> Skills => _skills;
        public IReadOnlyList<LanguageSkill> Languages => _languages;

        [JsonConstructor]
        private SkillSet(IReadOnlyList<Skill> skills,IReadOnlyList<LanguageSkill> languages)
        {
            _skills = [.. skills];
            _languages = [.. languages];
        }

        public static SkillSet Empty() => new([], []);

        public static SkillSet Create(IEnumerable<Skill> skills, IEnumerable<LanguageSkill> languages)
            => new(skills.ToList(), languages.ToList());

        public static SkillSet? From(IEnumerable<Skill>? skills, IEnumerable<LanguageSkill>? languages)
        {
            if (skills is null && languages is null)
                return null;

            return new(skills?.ToList() ?? [], languages?.ToList() ?? []);
        }

        // Skills manipulations
        internal SkillSet ClearSkills()
            => new([], _languages);

        internal SkillSet AddSkill(Skill skill)
            => new(_skills.Append(skill).ToList(), _languages);

        internal SkillSet RemoveSkill(Skill skill)
            => new(_skills.Where(s => !s.Equals(skill)).ToList(), _languages);

        internal SkillSet UpdateSkill(Skill oldSkill, Skill newSkill)
            => new(_skills.Select(s => s.Equals(oldSkill) ? newSkill : s).ToList(), _languages);

        internal SkillSet ReplaceSkills(IEnumerable<Skill> skills)
            => new(skills.ToList(), _languages);

        // Languages manipulations
        internal SkillSet ClearLanguages()
            => new(_skills, []);

        internal SkillSet AddLanguage(LanguageSkill language)
            => new(_skills, _languages.Append(language).ToList());

        internal SkillSet RemoveLanguage(LanguageSkill language)
            => new(_skills, _languages.Where(l => !l.Equals(language)).ToList());

        internal SkillSet UpdateLanguage(LanguageSkill oldLanguage, LanguageSkill newLanguage)
            => new(_skills, _languages.Select(l => l.Equals(oldLanguage) ? newLanguage : l).ToList());

        internal SkillSet ReplaceLanguages(IEnumerable<LanguageSkill> languages)
            => new(_skills, languages.ToList());

        // Equality
        public bool Equals(SkillSet? other) =>
            other is not null
            && _skills.Count == other._skills.Count
            && _languages.Count == other._languages.Count
            && _skills.OrderBy(s => s.Name).SequenceEqual(other._skills.OrderBy(s => s.Name))
            && _languages.OrderBy(l => l.Language).SequenceEqual(other._languages.OrderBy(l => l.Language));

        public override bool Equals(object? obj) => Equals(obj as SkillSet);

        public override int GetHashCode()
        {
            var hash = new HashCode();

            foreach (var skill in _skills)
                hash.Add(skill);

            foreach (var language in _languages)
                hash.Add(language);

            return hash.ToHashCode();
        }

    }
}
