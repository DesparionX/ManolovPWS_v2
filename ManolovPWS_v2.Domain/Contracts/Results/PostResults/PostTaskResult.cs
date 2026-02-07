using ManolovPWS_v2.Domain.Models.Post;
using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Results.PostResults
{
    public sealed class PostTaskResult(Post? value = default, IError? error = default) : ITaskResult<Post>
    {
        public Post? Value { get; } = value;

        public IError? Error { get; } = error;

        public bool IsSuccess => Error is null;
    }
}
