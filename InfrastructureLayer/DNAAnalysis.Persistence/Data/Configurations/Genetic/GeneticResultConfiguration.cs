using DNAAnalysis.Domain.Entities.GeneticModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DNAAnalysis.Persistence.Configurations.Genetic
{
    public class GeneticResultConfiguration : IEntityTypeConfiguration<GeneticResult>
    {
        public void Configure(EntityTypeBuilder<GeneticResult> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FatherStatus)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.MotherStatus)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.MessageToPatient)
                   .IsRequired();

            builder.Property(x => x.Advice)
                   .IsRequired();


                   builder.HasOne(x => x.GeneticRequest)
       .WithOne(x => x.Result)
       .HasForeignKey<GeneticResult>(x => x.GeneticRequestId);
        }
    }
}