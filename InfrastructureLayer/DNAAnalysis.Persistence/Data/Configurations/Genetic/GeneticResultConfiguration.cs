using DNAAnalysis.Domain.Entities.GeneticModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

            builder.Property(x => x.Explanation)
                   .IsRequired();

            // 🔹 Converter
            var converter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null)!
            );

            // 🔹 Comparer
            var comparer = new ValueComparer<List<string>>(
                (c1, c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
            );

            var adviceProperty = builder.Property(x => x.Advice);

            adviceProperty.HasConversion(converter);
            adviceProperty.Metadata.SetValueComparer(comparer);
            adviceProperty.IsRequired();

            // 🔹 Probabilities
            var probConverter = new ValueConverter<List<string>?, string?>(
                v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => v == null ? null : JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null)
            );

            var probComparer = new ValueComparer<List<string>?>(
                (c1, c2) => (c1 == null && c2 == null) || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
                c => c == null ? 0 : c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c == null ? null : c.ToList()
            );

            var probProperty = builder.Property(x => x.Probabilities);

            probProperty.HasConversion(probConverter);
            probProperty.Metadata.SetValueComparer(probComparer);

            builder.HasOne(x => x.GeneticRequest)
                   .WithOne(x => x.Result)
                   .HasForeignKey<GeneticResult>(x => x.GeneticRequestId);
        }
    }
}