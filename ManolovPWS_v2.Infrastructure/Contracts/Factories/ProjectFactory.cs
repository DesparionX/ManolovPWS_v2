using ManolovPWS_v2.Domain.Contracts.Factories;
using ManolovPWS_v2.Domain.Models.Project;
using ManolovPWS_v2.Infrastructure.Contracts.Maps;
using ManolovPWS_v2.Infrastructure.Persistance;

namespace ManolovPWS_v2.Infrastructure.Contracts.Factories
{
    public sealed class ProjectFactory(AppDbContext context) : IProjectFactory
    {
        private readonly AppDbContext _context = context;

        public async Task<Project?> CreateAsync(Project project, CancellationToken cancellationToken = default)
        {
            var entity = project.ToDbEntity();

            _context.Projects.Add(entity);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ? entity.ToDomain() : default;
        }
    }
}
