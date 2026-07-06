using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Shared.ReadModels;

namespace ManolovPWS_v2.Api.Contracts.Identity
{
    public sealed record SignInApiResponse(AccessToken AccessToken, CompactUserReadModel User);
}
