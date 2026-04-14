namespace DNAAnalysis.Domain.Entities.GeneticModule;

public class GeneticResult : BaseEntity<int>
{
    public string Summary { get; set; } = default!;

    public string Explanation { get; set; } = default!;

    public List<string> Advice { get; set; } = new();

    public List<string>? Probabilities { get; set; }

    public int GeneticRequestId { get; set; }
    public GeneticRequest GeneticRequest { get; set; } = default!;
}