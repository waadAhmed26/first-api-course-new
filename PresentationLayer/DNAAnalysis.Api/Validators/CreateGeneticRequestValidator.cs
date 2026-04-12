using FluentValidation;
using DNAAnalysis.Shared.GeneticRequestDtos;

namespace DNAAnalysis.Api.Validators;

public class CreateGeneticRequestValidator : AbstractValidator<CreateGeneticRequestDto>
{
    public CreateGeneticRequestValidator()
    {
        RuleFor(x => x.FatherFilePath)
            .NotEmpty()
            .WithMessage("Father file is required");

        RuleFor(x => x.MotherFilePath)
            .NotEmpty()
            .WithMessage("Mother file is required");
    }
}