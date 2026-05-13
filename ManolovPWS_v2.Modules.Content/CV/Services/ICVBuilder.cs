using ManolovPWS_v2.Domain.Models.Project;
using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Modules.Content.CV.Shared.ReadModels;

namespace ManolovPWS_v2.Modules.Content.CV.Services
{
    public interface ICVBuilder
    {
        public PublicCVReadModel Build(User user, IReadOnlyCollection<Project> projects);
    }
}
