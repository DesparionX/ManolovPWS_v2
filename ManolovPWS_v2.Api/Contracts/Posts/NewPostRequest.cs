namespace ManolovPWS_v2.Api.Contracts.Posts
{
    public sealed record NewPostRequest(
        string Title,
        string Context,
        string? Thumb,
        IEnumerable<string>? Gallery,
        bool IsPinned
        );
}
