namespace ManolovPWS_v2.Api.Contracts.Posts
{
    public sealed record UpdatePostGalleryRequest(IReadOnlyCollection<string> NewGallery);
}
