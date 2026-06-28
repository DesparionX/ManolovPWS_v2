using ManolovPWS_v2.Domain.Models.Project.Properties;
using ManolovPWS_v2.Domain.Models.Project.Properties.ProjectStack;

namespace ManolovPWS_v2.Modules.Projects.Project.Maps
{
    public static class DataTransferObjects
    {
        public static IEnumerable<ProjectPicture> ToProjectPictures(this IEnumerable<string> urls)
            => urls
                .Where(u => !string.IsNullOrWhiteSpace(u))
                .Select(ProjectPicture.Create);

        public static IEnumerable<StackTag> ToProjectStack(this IEnumerable<string> stackItems)
            => stackItems.Select(item => StackTag.Create(item));
    }
}
