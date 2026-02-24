using DNAAnalysis.Shared.GeneticRequestDtos;
using DNAAnalysis.Domain.Entities.GeneticModule;
using DNAAnalysis.Shared.Enums;

public interface IGeneticRequestService
{
    Task<int> CreateRequestAsync(string userId, CreateGeneticRequestDto dto);

    Task<IEnumerable<GeneticRequestDto>> GetUserRequestsAsync(string userId);

    Task<IEnumerable<GeneticRequestDto>> GetAllRequestsAsync();

    Task<GeneticRequestDto?> GetByIdAsync(int id);

    
    Task UpdateStatusAsync(int id, RequestStatus status);
}