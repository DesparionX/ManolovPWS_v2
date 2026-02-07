using ManolovPWS_v2.Domain.Models.Project;
using ManolovPWS_v2.Domain.Models.Project.Properties;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Repositories
{
    public interface IProjectRepository : IRepository<Project, ProjectId>
    {
        Task<ITaskResult<Project>> FindByOwner(UserId ownerId);
    }
}
