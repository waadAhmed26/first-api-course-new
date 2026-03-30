using DNAAnalysis.Shared.Enums;

namespace DNAAnalysis.Shared.DTOs.Alarm;

public class ReminderDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    
    public string? Description { get; set; }

    public ReminderType ReminderType { get; set; }

    
    public DateTime Date { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    public ReminderStatus Status { get; set; }
}