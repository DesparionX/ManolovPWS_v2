namespace ManolovPWS_v2.Api.Contracts.Projects
{
    public sealed record AddProjectRequest(
        string Name,
        string Description,
        string ProjectState,
        string? LiveUrl,
        string? GitHubUrl,
        IReadOnlyCollection<string>? GalleryPictures,
        IReadOnlyCollection<string> ProjectStack,
        string ThumbUrl);
}
