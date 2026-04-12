namespace DNAAnalysis.Shared.GeneticResultDtos;

public class CreateGeneticResultDto
{
    public string FatherStatus { get; set; } = default!;
    public string MotherStatus { get; set; } = default!;
    public string? ChildStatus { get; set; }

    public string Explanation { get; set; } = default!;

    public List<string> Advice { get; set; } = new();
}