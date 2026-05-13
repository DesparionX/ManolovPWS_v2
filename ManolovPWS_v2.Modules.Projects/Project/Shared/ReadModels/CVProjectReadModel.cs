namespace ManolovPWS_v2.Modules.Projects.Project.Shared.ReadModels
{
    public sealed record CVProjectReadModel(
        string Name,
        string Description,
        string State,
        string? LiveUrl,
        string? GitHubUrl,
        IReadOnlyList<string> Stack
        );
}
