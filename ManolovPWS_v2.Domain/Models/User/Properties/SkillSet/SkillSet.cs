using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.User.Properties.SkillSet
{
    public sealed class SkillSet : IEquatable<SkillSet>
    {
        private readonly List<Skill> _skills;
        private readonly List<LanguageSkill> _languages;

        public IReadOnlyCollection<Skill> Skills => _skills;
        public IReadOnlyCollection<LanguageSkill> Languages => _languages;

        private SkillSet(IEnumerable<Skill> skills,IEnumerable<LanguageSkill> languages)
        {
            _skills = [.. skills];
            _languages = [.. languages];
        }

        public static SkillSet Empty() => new([], []);

        public static SkillSet Create(IEnumerable<Skill> skills, IEnumerable<LanguageSkill> languages)
            => new(skills, languages);

        // Skills manipulations
        public SkillSet ClearSkills()
            => new([], _languages);

        public SkillSet AddSkill(Skill skill)
            => new(_skills.Append(skill), _languages);

        public SkillSet RemoveSkill(Skill skill)
            => new(_skills.Where(s => !s.Equals(skill)), _languages);

        public SkillSet UpdateSkill(Skill oldSkill, Skill newSkill)
            => new(_skills.Select(s => s.Equals(oldSkill) ? newSkill : s), _languages);

        public SkillSet UpdateSkills(IEnumerable<Skill> skills)
            => new(skills, _languages);

        // Languages manipulations
        public SkillSet ClearLanguages()
            => new(_skills, []);

        public SkillSet AddLanguage(LanguageSkill language)
            => new(_skills, _languages.Append(language));

        public SkillSet RemoveLanguage(LanguageSkill language)
            => new(_skills, _languages.Where(l => !l.Equals(language)));

        public SkillSet UpdateLanguage(LanguageSkill oldLanguage, LanguageSkill newLanguage)
            => new(_skills, _languages.Select(l => l.Equals(oldLanguage) ? newLanguage : l));

        public SkillSet UpdateLanguages(IEnumerable<LanguageSkill> languages)
            => new(_skills, languages);

        // Equality
        public bool Equals(SkillSet? other) =>
            other is not null
            && _skills.Count == other._skills.Count
            && _languages.Count == other._languages.Count
            && !_skills.OrderBy(s => s.Name).SequenceEqual(other._skills.OrderBy(s => s.Name))
            && !_languages.Except(other._languages).Any();

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
