namespace ManolovPWS_v2.Modules.Identity.User.Features.Shared.SharedProperties
{
    public sealed record Certificate(
        string Title,
        string Issuer,
        DateOnly DateObtained,
        string CredentialId,
        string CredentialUrl
        );
}
