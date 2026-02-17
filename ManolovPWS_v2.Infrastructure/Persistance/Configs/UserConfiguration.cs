using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Domain.Models.User.Properties.Certificates;
using ManolovPWS_v2.Domain.Models.User.Properties.Contacts;
using ManolovPWS_v2.Domain.Models.User.Properties.Education;
using ManolovPWS_v2.Domain.Models.User.Properties.Experience;
using ManolovPWS_v2.Domain.Models.User.Properties.SkillSet;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;
using ManolovPWS_v2.Infrastructure.Persistance.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace ManolovPWS_v2.Infrastructure.Persistance.Configs
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<DbUser>
    {
        public void Configure(EntityTypeBuilder<DbUser> user)
        {
            // Table name
            user.ToTable("Users");

            // Prevent auto generated IDs
            user.Property(u => u.Id)
                .ValueGeneratedNever();

            // Indexes, constraints, etc
            user.HasIndex(u => u.Email)
                .IsUnique();

            user.HasIndex(u => u.UserName)
                .IsUnique();

            // Basic properties
            user.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(20);

            user.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            user.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(20);

            user.Property(u => u.Profession)
                .IsRequired()
                .HasMaxLength(30);

            user.Property(u => u.Summary)
                .HasMaxLength(5000);

            // Gender as a string
            user.Property(u => u.Gender)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(10);

            // Birthdate as DateOnly
            user.Property(u => u.BirthDate)
                .HasColumnType("date");

            // JSON value objects
            user.Property(u => u.Address)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonOptions.Default),
                    v => JsonSerializer.Deserialize<Address>(v, JsonOptions.Default))
                .HasColumnType("jsonb");

            user.Property(u => u.Contacts)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonOptions.Default),
                    v => JsonSerializer.Deserialize<Contacts>(v, JsonOptions.Default))
                .HasColumnType("jsonb");

            user.Property(u => u.Skills)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonOptions.Default),
                    v => JsonSerializer.Deserialize<SkillSet>(v, JsonOptions.Default))
                .HasColumnType("jsonb");

            user.Property(u => u.Experience)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonOptions.Default),
                    v => JsonSerializer.Deserialize<Experience>(v, JsonOptions.Default))
                .HasColumnType("jsonb");

            user.Property(u => u.EducationHistory)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonOptions.Default),
                    v => JsonSerializer.Deserialize<EducationHistory>(v, JsonOptions.Default))
                .HasColumnType("jsonb");

            user.Property(u => u.Certificates)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonOptions.Default),
                    v => JsonSerializer.Deserialize<Certificates>(v, JsonOptions.Default))
                .HasColumnType("jsonb");
        }
    }
}
