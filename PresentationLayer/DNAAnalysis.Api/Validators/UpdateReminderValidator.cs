using DNAAnalysis.Shared.DTOs.Alarm;
using FluentValidation;

namespace DNAAnalysis.Api.Validators;

public class UpdateReminderValidator : AbstractValidator<UpdateReminderDto>
{
    public UpdateReminderValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.Date)
            .GreaterThanOrEqualTo(DateTime.Today);

        RuleFor(x => x.StartTime)
            .NotEmpty();

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .When(x => x.EndTime.HasValue);

        RuleFor(x => x.ReminderType)
            .IsInEnum();
    }
}