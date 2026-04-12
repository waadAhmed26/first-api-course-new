using AutoMapper;
using DNAAnalysis.Domain.Entities.AlarmModule;
using DNAAnalysis.Persistence.Data.DBContexts;
using DNAAnalysis.Services.Abstraction;
using DNAAnalysis.Shared.DTOs.Alarm;
using DNAAnalysis.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace DNAAnalysis.Services;

public class ReminderService : IReminderService
{
    private readonly DNAAnalysisDbContext _context;
    private readonly IMapper _mapper;

    public ReminderService(DNAAnalysisDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReminderDto> CreateAsync(string userId, CreateReminderDto dto)
    {
        var reminder = _mapper.Map<Reminder>(dto);

        reminder.UserId = userId;
        reminder.CreatedAt = DateTime.UtcNow;
        reminder.Status = ReminderStatus.Pending;

        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();

        return _mapper.Map<ReminderDto>(reminder);
    }

    public async Task<IEnumerable<ReminderDto>> GetAllAsync(string userId)
    {
        var reminders = await _context.Reminders
            .Where(r => r.UserId == userId && !r.IsDeleted)
            .OrderBy(r => r.Date)
            .ThenBy(r => r.StartTime)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ReminderDto>>(reminders);
    }

    public async Task<IEnumerable<ReminderDto>> GetByStatusAsync(string userId, ReminderStatus status)
    {
        var reminders = await _context.Reminders
            .Where(r => r.UserId == userId &&
                        r.Status == status &&
                        !r.IsDeleted)
            .OrderBy(r => r.Date)
            .ThenBy(r => r.StartTime)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ReminderDto>>(reminders);
    }

    public async Task<ReminderDto?> GetByIdAsync(int id, string userId)
    {
        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.Id == id &&
                                      r.UserId == userId &&
                                      !r.IsDeleted);

        return reminder == null ? null : _mapper.Map<ReminderDto>(reminder);
    }

    public async Task<bool> UpdateAsync(int id, string userId, UpdateReminderDto dto)
    {
        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.Id == id &&
                                      r.UserId == userId &&
                                      !r.IsDeleted);

        if (reminder == null)
    throw new ArgumentException("Reminder not found");

        _mapper.Map(dto, reminder);
        reminder.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.Id == id &&
                                      r.UserId == userId &&
                                      !r.IsDeleted);

       if (reminder == null)
    throw new ArgumentException("Reminder not found");

        reminder.IsDeleted = true;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<ReminderDto>> GetByDateAsync(string userId, DateTime date)
    {
        var reminders = await _context.Reminders
            .Where(r => r.UserId == userId &&
                        r.Date.Date == date.Date &&
                        !r.IsDeleted)
            .OrderBy(r => r.StartTime)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ReminderDto>>(reminders);
    }

    public async Task<bool> MarkAsCompleteAsync(int id, string userId)
    {
        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.Id == id &&
                                      r.UserId == userId &&
                                      !r.IsDeleted);

        if (reminder == null)
    throw new ArgumentException("Reminder not found");

        reminder.Status = ReminderStatus.Completed;
        reminder.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}