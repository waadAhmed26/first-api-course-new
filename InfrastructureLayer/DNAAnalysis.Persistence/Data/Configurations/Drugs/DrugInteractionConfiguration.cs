using DNAAnalysis.Domain.Entities.DrugModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DNAAnalysis.Persistence.Data.Configurations.Drugs
{
    public class DrugInteractionConfiguration 
        : IEntityTypeConfiguration<DrugInteraction>
    {
        public void Configure(EntityTypeBuilder<DrugInteraction> builder)
        {
            builder.Property(d => d.Drug1)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(d => d.Drug2)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(d => d.Severity)
                .HasMaxLength(100);

            builder.Property(d => d.Description)
                .HasMaxLength(1000);

            builder.Property(d => d.UserId)
                .IsRequired();
        }
    }
}