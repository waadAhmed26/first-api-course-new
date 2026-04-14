using DNAAnalysis.Shared.Enums;

namespace DNAAnalysis.Shared.GeneticRequestDtos;

public class CreateGeneticRequestDto
{
    public string? FatherFilePath { get; set; }
    public string? MotherFilePath { get; set; }

    // ✅ rename for clarity
    public string? IndividualFilePath { get; set; }

    public TestType TestType { get; set; }
}