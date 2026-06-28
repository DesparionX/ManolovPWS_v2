namespace ManolovPWS_v2.Api.Contracts.Projects
{
    public sealed record UpdateProjectStackRequest(IReadOnlyCollection<string> NewStack);
}
