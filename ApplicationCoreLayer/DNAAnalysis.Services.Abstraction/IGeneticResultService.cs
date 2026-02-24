using DNAAnalysis.Shared.GeneticResultDtos;

namespace DNAAnalysis.Services.Abstraction
{
    public interface IGeneticResultService
    {
        Task AddResultAsync(int requestId, CreateGeneticResultDto dto);

        Task<GeneticResultDto?> GetResultByRequestIdAsync(int requestId);
    }
}