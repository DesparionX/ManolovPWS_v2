using ManolovPWS_v2.Domain.Models.Post;
using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Results.PostResults
{
    public sealed class PostTaskResult(Post? value = default, IReadOnlyList<Post>? collection = default, IReadOnlyList<IError>? errors = default) : ITaskResult<Post>
    {
        public Post? Value { get; } = value;

        public IReadOnlyList<Post>? Collection { get; } = collection;

        public IReadOnlyList<IError>? Errors { get; } = errors;

        public bool IsSuccess => Errors is null;
    }
}
