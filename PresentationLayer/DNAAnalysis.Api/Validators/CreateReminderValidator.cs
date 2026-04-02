using DNAAnalysis.Shared.DTOs.Alarm;
using FluentValidation;

namespace DNAAnalysis.Api.Validators;

public class CreateReminderValidator : AbstractValidator<CreateReminderDto>
{
    public CreateReminderValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Title is required and must not exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.Date)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("Date cannot be in the past");

        RuleFor(x => x.StartTime)
            .NotEmpty()
            .WithMessage("Start time is required");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .When(x => x.EndTime.HasValue)
            .WithMessage("End time must be after start time");

        RuleFor(x => x.ReminderType)
            .IsInEnum()
            .WithMessage("Invalid reminder type");
    }
}