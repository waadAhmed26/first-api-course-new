using DNAAnalysis.Domain.Entities;

namespace DNAAnalysis.Domain.Entities.DrugModule
{
    
     public class DrugInteraction : BaseEntity<int>
    {
        public string Drug1 { get; set; } = default!;

        public string Drug2 { get; set; } = default!;

        public bool HasInteraction { get; set; }

        public string? Severity { get; set; }

        public string? Description { get; set; }

        // Foreign Key
        public string UserId { get; set; } = default!;
   
    }
}