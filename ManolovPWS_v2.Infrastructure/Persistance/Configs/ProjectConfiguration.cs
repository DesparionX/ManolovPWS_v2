using ManolovPWS_v2.Domain.Models.Project.Properties;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;
using ManolovPWS_v2.Infrastructure.Persistance.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace ManolovPWS_v2.Infrastructure.Persistance.Configs
{
    public sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> project)
        {
            // Table name
            project.ToTable("Projects");

            // Prevent auto generated IDs
            project.Property(p => p.Id)
                .ValueGeneratedNever();

            // Relationships
            project.HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Basic properties
            project.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            project.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(10000);

            // Project state as string
            project.Property(p => p.ProjectState)
                .IsRequired()
                .HasMaxLength(20)
                .HasConversion<string>();

            // Dates as DateOnly
            project.Property(p => p.UploadedDate)
                .IsRequired()
                .HasColumnType("date");

            project.Property(p => p.UpdatedDate)
                .HasColumnType("date");

            // JSON for gallery
            project.Property(p => p.Gallery)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonOptions.Default),
                    v => JsonSerializer.Deserialize<ProjectGallery>(v, JsonOptions.Default)!)
                .HasColumnType("jsonb");
        }
    }
}
