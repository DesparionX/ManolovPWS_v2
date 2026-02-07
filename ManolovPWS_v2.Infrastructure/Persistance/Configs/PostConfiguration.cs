using ManolovPWS_v2.Domain.Models.Post.Properties.PostContent;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;
using ManolovPWS_v2.Infrastructure.Persistance.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ManolovPWS_v2.Infrastructure.Persistance.Configs
{
    public sealed class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> post)
        {
            // Table name
            post.ToTable("Posts");

            // Prevent auto generated IDs
            post.Property(p => p.Id)
                .ValueGeneratedNever();

            // Relationships
            post.HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Basic properties
            post.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(50);

            // Dates configuration
            post.Property(p => p.PublishedDate)
                .IsRequired()
                .HasColumnType("date");

            post.Property(p => p.UpdatedDate)
                .HasColumnType("date");

            // JSON serialization for PostContent
            post.Property(p => p.Content)
                .IsRequired()
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonOptions.Default),
                    v => JsonSerializer.Deserialize<PostContent>(v, JsonOptions.Default)!)
                .HasColumnType("jsonb");
        }
    }
}
