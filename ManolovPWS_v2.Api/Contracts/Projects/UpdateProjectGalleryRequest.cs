namespace ManolovPWS_v2.Api.Contracts.Projects
{
    public sealed record UpdateProjectGalleryRequest(IReadOnlyCollection<string> NewGallery);
}
