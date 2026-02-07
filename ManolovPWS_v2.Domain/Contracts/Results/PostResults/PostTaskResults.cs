using ManolovPWS_v2.Domain.Models.Post;
using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Results.PostResults
{
    public static class PostTaskResults
    {
        public static ITaskResult Success()
            => new PostTaskResult();

        public static ITaskResult<Post> Success(Post post)
            => new PostTaskResult(value: post);

        public static ITaskResult Failure(PostError error)
            => new PostTaskResult(error: error);
    }
}
