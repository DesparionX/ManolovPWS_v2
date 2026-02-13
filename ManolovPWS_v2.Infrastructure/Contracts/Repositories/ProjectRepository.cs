using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Project;
using ManolovPWS_v2.Domain.Models.Project.Properties;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Infrastructure.Contracts.Maps;
using ManolovPWS_v2.Infrastructure.Contracts.Results;
using ManolovPWS_v2.Infrastructure.Exceptions;
using ManolovPWS_v2.Infrastructure.Persistance;
using ManolovPWS_v2.Shared.Abstractions.Results;
using Microsoft.EntityFrameworkCore;

namespace ManolovPWS_v2.Infrastructure.Contracts.Repositories
{
    public sealed class ProjectRepository(AppDbContext context) : IProjectRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var projects = await _context.Projects.ToListAsync(cancellationToken);

            return projects.ToDomainList();
        }

        public async Task<Project> FindByIdAsync(ProjectId id, CancellationToken cancellationToken = default)
        {
            var project = await _context.Projects.FindAsync([id.Value], cancellationToken: cancellationToken) 
                ?? throw DbExceptions.ProjectNotFound(id.Value);

            return project.ToDomain();
        }

        public async Task<IReadOnlyList<Project>> FindByOwner(UserId ownerId, CancellationToken cancellationToken = default)
        {
            var userProjects = await _context.Projects.Where(p => p.OwnerId.Equals(ownerId.Value)).ToListAsync(cancellationToken)
                ?? throw DbExceptions.EmptyProjectListForUser(ownerId.Value);

            return userProjects.ToDomainList();
        }

        public async Task<ITaskResult> RemoveAsync(ProjectId id, CancellationToken cancellationToken = default)
        {
            var project = await _context.Projects.FindAsync([id.Value], cancellationToken: cancellationToken) 
                ?? throw DbExceptions.ProjectNotFound(id.Value);

            _context.Projects.Remove(project);

            await _context.SaveChangesAsync(cancellationToken);

            return InfraTaskResults.Success();
        }

        public async Task<ITaskResult> SaveAsync(Project entity, CancellationToken cancellationToken = default)
        {
            var projectEntity = entity.ToDbEntity();
            
            _context.Projects.Update(projectEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return InfraTaskResults.Success();
        }
    }
}
