using ManolovPWS_v2.Modules.Identity.User.Shared.SharedProperties;

namespace ManolovPWS_v2.Api.Contracts.Profile
{
    public sealed record UpdateCertificatesRequest(IEnumerable<CertificateDto> Certificates);
}
