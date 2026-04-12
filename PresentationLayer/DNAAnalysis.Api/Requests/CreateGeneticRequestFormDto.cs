using Microsoft.AspNetCore.Http;
using DNAAnalysis.Shared.Enums;

namespace DNAAnalysis.Api.Requests;

public class CreateGeneticRequestFormDto
{
    public IFormFile? FatherFile { get; set; }
    public IFormFile? MotherFile { get; set; }
    public IFormFile? ChildFile { get; set; }

    // ✅ NEW
    public TestType TestType { get; set; }
}