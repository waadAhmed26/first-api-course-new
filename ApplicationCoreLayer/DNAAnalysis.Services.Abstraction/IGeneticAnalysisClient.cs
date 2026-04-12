using DNAAnalysis.Shared.GeneticResultDtos;
using DNAAnalysis.Shared.Enums;

namespace DNAAnalysis.Services.Abstraction;

public interface IGeneticAnalysisClient
{
    Task<GeneticResultDto> AnalyzeAsync(
        string fatherPath,
        string motherPath,
        string? childPath,
        TestType testType); // ✅ NEW
}