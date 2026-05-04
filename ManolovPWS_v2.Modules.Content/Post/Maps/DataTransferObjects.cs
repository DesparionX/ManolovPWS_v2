using ManolovPWS_v2.Domain.Models.Post.Properties.PostContent;

namespace ManolovPWS_v2.Modules.Content.Post.Maps
{
    public static class DataTransferObjects
    {
        public static IEnumerable<PostPicture> ToPostPictures(this IEnumerable<string> pictureUrls)
            => pictureUrls.Select(PostPicture.Create);
    }
}
