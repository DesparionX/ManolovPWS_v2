using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Project;
using ManolovPWS_v2.Domain.Models.Project.Properties;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Infrastructure.Contracts.Repositories
{
    public sealed class ProjectRepository : IProjectRepository
    {
        public Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Project> FindByIdAsync(ProjectId id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Project> FindByOwner(UserId ownerId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ITaskResult> RemoveAsync(ProjectId id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ITaskResult> SaveAsync(Project entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
