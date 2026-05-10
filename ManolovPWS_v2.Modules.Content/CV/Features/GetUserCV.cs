using ManolovPWS_v2.Modules.Content.CV.Shared.ReadModels;
using ManolovPWS_v2.Shared.Abstractions.CQRS;

namespace ManolovPWS_v2.Modules.Content.CV.Features
{
    public sealed record GetUserCVQuery() : IQuery<PublicCVReadModel>;
}
