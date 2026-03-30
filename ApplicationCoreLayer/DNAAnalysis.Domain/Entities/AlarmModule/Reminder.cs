using DNAAnalysis.Domain;
using DNAAnalysis.Shared.Enums;

namespace DNAAnalysis.Domain.Entities.AlarmModule;

public class Reminder : BaseEntity<int>
{
    public string UserId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public ReminderType ReminderType { get; set; }

    
    public DateTime Date { get; set; }

    // ✅ بدل Time
    public TimeSpan StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    // ✅ بدل IsCompleted
    public ReminderStatus Status { get; set; } = ReminderStatus.Pending;
    
    
    // public ApplicationUser User { get; set; }
}