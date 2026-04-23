using ManolovPWS_v2.Domain.Models.User.Exceptions;

namespace ManolovPWS_v2.Domain.Models.User.Properties.SkillSet
{
    public enum SkillType
    {
        Tech = 1,
        Soft = 2,
    }

    public static class SkillTypeExtensions
    {
        public static SkillType FromString(string type)
        {
            if(!Enum.TryParse<SkillType>(type, ignoreCase: true, out var result))
                throw new InvalidSkillException($"Invalid skill type: {type}");

            return result;
        }
    }
}
