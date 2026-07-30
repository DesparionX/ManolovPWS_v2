using ManolovPWS_v2.Domain.Models.Message.Properties.SenderInfo;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;
using ManolovPWS_v2.Infrastructure.Persistance.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace ManolovPWS_v2.Infrastructure.Persistance.Configs
{
    public sealed class MessageConfiguration : IEntityTypeConfiguration<DbMessage>
    {
        public void Configure(EntityTypeBuilder<DbMessage> message)
        {
            message.ToTable("Messages");

            // PK Properties
            message.HasKey(m => m.Id);

            message.HasIndex(m => m.Id);

            message.Property(m => m.Id)
                .ValueGeneratedNever();


            // Message data properties
            message.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(50);

            message.Property(m => m.Context)
                .IsRequired()
                .HasMaxLength(10000);

            // Message sender properties
            message.Property(m => m.SenderName)
                .IsRequired()
                .HasMaxLength(30);

            message.Property(m => m.SenderEmail)
                .IsRequired()
                .HasMaxLength(256);

            message.Property(m => m.SenderMetadata)
                .IsRequired()
                .HasConversion(
                    m => JsonSerializer.Serialize(m, JsonOptions.Default),
                    m => JsonSerializer.Deserialize<SenderMetadata>(m, JsonOptions.Default)!)
                .HasColumnType("jsonb");

            // Message date properties
            message.Property(m => m.SentDate)
                .IsRequired()
                .HasColumnType("timestamptz");

            // Message status properties
            message.Property(m => m.IsUnread)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}
