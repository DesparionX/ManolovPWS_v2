using ManolovPWS_v2.Domain.Contracts.Factories;
using ManolovPWS_v2.Domain.Models.Post;
using ManolovPWS_v2.Infrastructure.Contracts.Maps;
using ManolovPWS_v2.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Infrastructure.Contracts.Factories
{
    public sealed class PostFactory(AppDbContext context) : IPostFactory
    {
        private readonly AppDbContext _context = context;

        public async Task<Post?> CreateAsync(Post entity, CancellationToken cancellationToken = default)
        {
            var dbEntity = entity.ToDbEntity();

            _context.Posts.Add(dbEntity);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ? dbEntity.ToDomain() : null;
        }
    }
}
