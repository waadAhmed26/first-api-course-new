namespace DNAAnalysis.Shared.GeneticResultDtos;

public class GeneticResultDto
{
    public string Summary { get; set; } = default!;

    public string FatherStatus { get; set; } = default!;
    public string MotherStatus { get; set; } = default!;
    public string? ChildStatus { get; set; }

    public string Explanation { get; set; } = default!;

    public List<string> Advice { get; set; } = new();

    public List<string>? Probabilities { get; set; }
}