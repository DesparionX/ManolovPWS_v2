namespace ManolovPWS_v2.Modules.Content.Post.Shared.ReadModels
{
    public sealed record PostReadModel(
        string Id,
        string AuthorId,
        string Title,
        string Context,
        string? Thumb,
        IEnumerable<string> Gallery,
        DateOnly PublishedDate,
        DateOnly? UpdatedDate,
        bool IsPinned
    );
}
