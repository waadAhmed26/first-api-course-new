using FluentValidation;
using DNAAnalysis.Api.Requests;
using DNAAnalysis.Shared.Enums;
using Microsoft.AspNetCore.Http;

namespace DNAAnalysis.Api.Validators
{
    public class CreateGeneticRequestFormValidator 
        : AbstractValidator<CreateGeneticRequestFormDto>
    {
        private readonly string[] AllowedExtensions = { ".txt" };

        public CreateGeneticRequestFormValidator()
        {
            RuleFor(x => x)
                .Custom((request, context) =>
                {
                    var father = NormalizeFile(request.FatherFile);
                    var mother = NormalizeFile(request.MotherFile);
                    var individual = NormalizeFile(request.IndividualFile);

                    // ================= MoreThanOne =================
                    if (request.TestType == TestType.MoreThanOne)
                    {
                        // ❌ رفع Individual بس
                        if (individual != null && father == null && mother == null)
                        {
                            context.AddFailure("This test requires father and mother files. Individual file is not allowed.");
                        }

                        // ❌ رفع Individual مع الأب/الأم
                        if (individual != null)
                        {
                            context.AddFailure("Individual file is not allowed in this test.");
                        }

                        // ❌ مفيش حاجة خالص
                        if (father == null && mother == null)
                        {
                            context.AddFailure("Father file is required.");
                            context.AddFailure("Mother file is required.");
                        }

                        // ❌ ناقص واحد
                        else
                        {
                            if (father == null)
                                context.AddFailure("Father file is required.");

                            if (mother == null)
                                context.AddFailure("Mother file is required.");
                        }

                        // ✅ validate type
                        if (father != null && !IsValidExtension(father))
                            context.AddFailure("Father file type is invalid. Only .txt allowed.");

                        if (mother != null && !IsValidExtension(mother))
                            context.AddFailure("Mother file type is invalid. Only .txt allowed.");
                    }

                    // ================= Individual =================
                    else if (request.TestType == TestType.Individual)
                    {
                        // ❌ رفع أب/أم بس
                        if ((father != null || mother != null) && individual == null)
                        {
                            context.AddFailure("This test requires only your file. Father or Mother files are not allowed.");
                        }

                        // ❌ رفع كله مع بعض
                        if (father != null || mother != null)
                        {
                            context.AddFailure("Father or Mother files are not allowed in Individual test.");
                        }

                        // ❌ مفيش حاجة
                        if (individual == null)
                        {
                            context.AddFailure("Individual file is required.");
                        }

                        // ✅ validate type
                        if (individual != null && !IsValidExtension(individual))
                        {
                            context.AddFailure("Invalid file type. Only .txt allowed.");
                        }
                    }
                });
        }

        private IFormFile? NormalizeFile(IFormFile? file)
        {
            if (file != null && file.Length == 0)
                return null;

            return file;
        }

        private bool IsValidExtension(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            return AllowedExtensions.Contains(extension);
        }
    }
}