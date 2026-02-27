namespace ManolovPWS_v2.Modules.Identity.User.Shared.SharedProperties
{
    public sealed record Language(
        string LanguageName,
        string? ReadingLevel,
        string? WritingLevel,
        string? SpeakingLevel,
        bool IsNative
        );
}
