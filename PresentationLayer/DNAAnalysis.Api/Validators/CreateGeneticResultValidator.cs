using FluentValidation;
using DNAAnalysis.Shared.GeneticResultDtos;

namespace DNAAnalysis.Api.Validators;

public class CreateGeneticResultValidator : AbstractValidator<CreateGeneticResultDto>
{
    public CreateGeneticResultValidator()
    {
        RuleFor(x => x.FatherStatus)
            .NotEmpty()
            .WithMessage("Father status is required.");

        RuleFor(x => x.MotherStatus)
            .NotEmpty()
            .WithMessage("Mother status is required.");

        RuleFor(x => x.Explanation)
            .NotEmpty()
            .WithMessage("Explanation is required.");

        RuleFor(x => x.Advice)
            .NotEmpty()
            .WithMessage("Advice list cannot be empty.");

        RuleForEach(x => x.Advice)
            .NotEmpty()
            .WithMessage("Advice item cannot be empty.");
    }
}