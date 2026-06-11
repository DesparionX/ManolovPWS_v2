namespace ManolovPWS_v2.Modules.Identity.User.Shared.SharedProperties
{
    public sealed record CertificateDto(
        string Title,
        string Issuer,
        DateOnly DateObtained,
        string CredentialId,
        string CredentialUrl
        );
}
