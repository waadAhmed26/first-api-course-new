using DNAAnalysis.Shared.GeneticResultDtos;
using DNAAnalysis.Shared.Enums;

namespace DNAAnalysis.Services.Abstraction;

public interface IGeneticAnalysisClient
{
    Task<GeneticResultDto> AnalyzeAsync(
        string? fatherPath,   // ✅ nullable
        string? motherPath,   // ✅ nullable
        string? childPath,
        TestType testType);
}