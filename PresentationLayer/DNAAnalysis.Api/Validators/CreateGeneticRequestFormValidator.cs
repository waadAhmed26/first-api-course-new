using FluentValidation;
using DNAAnalysis.Api.Requests;

namespace DNAAnalysis.Api.Validators
{
    public class CreateGeneticRequestFormValidator 
        : AbstractValidator<CreateGeneticRequestFormDto>
    {
        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB
        private readonly string[] AllowedExtensions = { ".txt", ".csv", ".vcf" };

        public CreateGeneticRequestFormValidator()
        {
            // ✅ Required files
            RuleFor(x => x.FatherFile)
                .NotNull()
                .WithMessage("Father file is required.");

            RuleFor(x => x.MotherFile)
                .NotNull()
                .WithMessage("Mother file is required.");

            // ✅ Validation
            RuleFor(x => x.FatherFile!)
                .Must(BeValidFile)
                .WithMessage(GetFileErrorMessage("Father file"));

            RuleFor(x => x.MotherFile!)
                .Must(BeValidFile)
                .WithMessage(GetFileErrorMessage("Mother file"));

            When(x => x.ChildFile != null, () =>
            {
                RuleFor(x => x.ChildFile!)
                    .Must(BeValidFile)
                    .WithMessage(GetFileErrorMessage("Child file"));
            });
        }

        private bool BeValidFile(IFormFile file)
        {
            if (file.Length == 0 || file.Length > MaxFileSize)
                return false;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return AllowedExtensions.Contains(extension);
        }

        private string GetFileErrorMessage(string fileName)
        {
            return $"{fileName} is invalid. Allowed types: (.txt, .csv, .vcf) and max size is 5 MB.";
        }
    }
}