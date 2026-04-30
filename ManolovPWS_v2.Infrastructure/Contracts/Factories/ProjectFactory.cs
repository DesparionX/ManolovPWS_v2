using ManolovPWS_v2.Domain.Contracts.Factories;
using ManolovPWS_v2.Domain.Models.Project;
using ManolovPWS_v2.Infrastructure.Contracts.Maps;
using ManolovPWS_v2.Infrastructure.Contracts.Results;
using ManolovPWS_v2.Infrastructure.Persistance;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Infrastructure.Contracts.Factories
{
    public sealed class ProjectFactory(AppDbContext context) : IProjectFactory
    {
        private readonly AppDbContext _context = context;

        public async Task<ITaskResult<Project>> CreateAsync(Project project, CancellationToken cancellationToken = default)
        {
            var entity = project.ToDbEntity();

            _context.Projects.Add(entity);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ? 
                Result<Project>.Success(entity.ToDomain()) 
                : Result<Project>.Failure([new InfraError(Code: "ProjectCreationFailed", Message: "Failed to create the project.")]);
        }
    }
}
