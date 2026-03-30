using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DNAAnalysis.Domain.Entities.AlarmModule;
using DNAAnalysis.Shared.Enums;

namespace DNAAnalysis.Persistence.Data.Configurations.Alarm;

public class ReminderConfiguration : IEntityTypeConfiguration<Reminder>
{
    public void Configure(EntityTypeBuilder<Reminder> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(r => r.Description)
               .HasMaxLength(500);

        builder.Property(r => r.UserId)
               .IsRequired();

        builder.Property(r => r.Date)
               .IsRequired();

        builder.Property(r => r.StartTime)
               .IsRequired();

        builder.Property(r => r.EndTime);

        builder.Property(r => r.ReminderType)
               .IsRequired();

        builder.Property(r => r.Status)
               .IsRequired()
               .HasDefaultValue(ReminderStatus.Pending);

        builder.Property(r => r.IsDeleted)
               .HasDefaultValue(false);

        builder.HasIndex(r => new { r.UserId, r.Date });
    }
}
