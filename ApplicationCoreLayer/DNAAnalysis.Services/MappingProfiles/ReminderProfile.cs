using AutoMapper;
using DNAAnalysis.Domain.Entities.AlarmModule;
using DNAAnalysis.Shared.DTOs.Alarm;

namespace DNAAnalysis.Services.MappingProfiles;

public class ReminderProfile : Profile
{
    public ReminderProfile()
    {
        CreateMap<Reminder, ReminderDto>();
        CreateMap<CreateReminderDto, Reminder>();
        CreateMap<UpdateReminderDto, Reminder>();
    }
}