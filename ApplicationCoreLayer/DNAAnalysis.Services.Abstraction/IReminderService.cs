using DNAAnalysis.Shared.DTOs.Alarm;
using DNAAnalysis.Shared.Enums;

namespace DNAAnalysis.Services.Abstraction;

public interface IReminderService
{
    Task<ReminderDto> CreateAsync(string userId, CreateReminderDto dto);

    Task<IEnumerable<ReminderDto>> GetAllAsync(string userId);

    Task<IEnumerable<ReminderDto>> GetByStatusAsync(string userId, ReminderStatus status);

    Task<ReminderDto?> GetByIdAsync(int id, string userId);

    Task<bool> UpdateAsync(int id, string userId, UpdateReminderDto dto);

    Task<bool> DeleteAsync(int id, string userId);

    Task<IEnumerable<ReminderDto>> GetByDateAsync(string userId, DateTime date);

    Task<bool> MarkAsCompleteAsync(int id, string userId);
}