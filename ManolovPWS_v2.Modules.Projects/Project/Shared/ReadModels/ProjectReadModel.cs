namespace ManolovPWS_v2.Modules.Projects.Project.Shared.ReadModels
{
    public sealed record ProjectReadModel(
        string Id,
        string OwnerId,
        string Name,
        string Description,
        string State,
        string? LiveUrl,
        string? GitHubUrl,
        DateOnly UploadedDate,
        DateOnly? UpdatedDate,
        IReadOnlyList<string> Gallery,
        string Thumb,
        IReadOnlyList<string> Stack
        );
}
