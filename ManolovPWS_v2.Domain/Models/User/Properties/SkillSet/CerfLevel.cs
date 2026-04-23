using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.User.Properties.SkillSet
{
    // Common European Framework of Reference for Languages
    public enum CerfLevel
    {
        A1 = 1,
        A2 = 2,
        B1 = 3,
        B2 = 4,
        C1 = 5,
        C2 = 6,
    }

    public static class CerfLevelExtensions
    {
        public static CerfLevel FromString(string level)
        {
            if (!Enum.TryParse(level, ignoreCase: true, out CerfLevel result))
                throw new InvalidSkillException($"Invalid CEFR level: {level}");

            return result;
        }
    }
}
