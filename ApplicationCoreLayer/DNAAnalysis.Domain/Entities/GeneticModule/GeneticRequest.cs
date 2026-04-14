using DNAAnalysis.Shared.Enums;

namespace DNAAnalysis.Domain.Entities.GeneticModule
{
    public class GeneticRequest : BaseEntity<int>
    {
        public string? FatherFilePath { get; set; } = default!;
        public string? MotherFilePath { get; set; } = default!;
        public string? ChildFilePath { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        // ✅ NEW
        public TestType TestType { get; set; }

        public string UserId { get; set; } = default!;

        public GeneticResult? Result { get; set; }
    }
}