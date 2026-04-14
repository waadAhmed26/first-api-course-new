using DNAAnalysis.Services.Abstraction;
using DNAAnalysis.Shared.GeneticResultDtos;
using DNAAnalysis.Shared.Enums;

namespace DNAAnalysis.Services;

public class FakeGeneticAnalysisClient : IGeneticAnalysisClient
{
    public async Task<GeneticResultDto> AnalyzeAsync(
        string? fatherPath,
        string? motherPath,
        string? individualPath,
        TestType testType)
    {
        await Task.Delay(1000);
        var random = new Random();

        // ================= MoreThanOne =================
        var married = new List<GeneticResultDto>
        {
            new()
            {
                Summary = "Both parents are healthy",
                Explanation = @"No mutation was detected in the CFTR gene in either parent.
Therefore, the risk of transmitting the disease to the child is excluded.",
                Advice = new()
                {
                    "Continue routine medical check-ups prior to pregnancy.",
                    "Genetic testing is not required unless there is a documented family history of the disease."
                }
            },
            new()
            {
                Summary = "Both parents are carriers of the mutation",
                Probabilities = new()
                {
                    "25% chance the child will be healthy.",
                    "25% chance the child will have the disease.",
                    "50% chance the child will be a carrier."
                },
                Explanation = @"Each parent passes on one copy of the gene, which creates different possible outcomes for the child.",
                Advice = new()
                {
                    "Genetic testing for the newborn or child is recommended.",
                    "Continue pregnancy follow-up with a genetic specialist."
                }
            },
            new()
            {
                Summary = "Both parents have the disease",
                Explanation = @"Both parents carry a mutation in the CFTR gene, which guarantees transmission of the disease to the child.",
                Advice = new()
                {
                    "Please consult a genetic doctor for guidance.",
                    "You can also use the app to follow medical and nutrition support."
                }
            }
        };

        // ================= Individual =================
        var individual = new List<GeneticResultDto>
        {
            new()
            {
                Summary = "You are healthy",
                Explanation = @"No mutation was detected in the CFTR gene.
You are not a carrier and not affected.",
                Advice = new()
                {
                    "Continue routine medical check-ups.",
                    "No further genetic testing is required unless advised by a doctor."
                }
            },
            new()
            {
                Summary = "You have the disease",
                Explanation = @"A mutation in the CFTR gene has been detected indicating that you are affected.",
                Advice = new()
                {
                    "Please consult a genetic doctor for proper diagnosis.",
                    "Follow medical and nutritional guidance."
                }
            },
            new()
            {
                Summary = "You are a carrier of the mutation",
                Explanation = @"You carry one mutated gene copy but do not show symptoms.",
                Advice = new()
                {
                    "Genetic counseling is recommended before marriage.",
                    "Partner testing is advised."
                }
            }
        };

        // ================= FINAL =================
        if (testType == TestType.MoreThanOne)
            return married[random.Next(married.Count)];

        if (testType == TestType.Individual)
            return individual[random.Next(individual.Count)];

        throw new Exception("Invalid Test Type provided.");
    }
}