using DNAAnalysis.Services.Abstraction;
using DNAAnalysis.Shared.GeneticResultDtos;
using DNAAnalysis.Shared.Enums;

namespace DNAAnalysis.Services;

public class FakeGeneticAnalysisClient : IGeneticAnalysisClient
{
    public async Task<GeneticResultDto> AnalyzeAsync(
        string fatherPath,
        string motherPath,
        string? childPath,
        TestType testType)
    {
        await Task.Delay(1000);

        var random = new Random();

        // ================= Married =================
        var marriedScenarios = new List<GeneticResultDto>
        {
            new GeneticResultDto
            {
                Summary = "Both parents are healthy",
                FatherStatus = "Healthy",
                MotherStatus = "Healthy",
                Explanation = "No mutation was detected in the CFTR gene in either parent. Therefore, the risk of transmitting the disease to the child is excluded.",
                Advice = new List<string>
                {
                    "Continue routine medical check-ups prior to pregnancy.",
                    "Genetic testing is not required unless there is a documented family history of the disease."
                }
            },

            new GeneticResultDto
            {
                Summary = "Both parents are carriers of the mutation",
                FatherStatus = "Carrier",
                MotherStatus = "Carrier",
                Probabilities = new List<string>
                {
                    "25% chance the child will be healthy.",
                    "25% chance the child will have the disease.",
                    "50% chance the child will be a carrier of the mutation."
                },
                Explanation = "Each parent passes on one copy of the gene, which creates different possible outcomes for the child.",
                Advice = new List<string>
                {
                    "Genetic testing for the newborn or child is recommended.",
                    "Continue pregnancy follow-up with a genetic specialist."
                }
            },

            new GeneticResultDto
            {
                Summary = "Both parents have the disease",
                FatherStatus = "Affected",
                MotherStatus = "Affected",
                Explanation = "Both parents carry a change in the CFTR gene, which causes the disease to be passed on to the child.",
                Advice = new List<string>
                {
                    "Please consult a genetic doctor for guidance.",
                    "You can also use the app to follow medical and nutrition support."
                }
            }
        };

        // ================= Single =================
        var singleScenarios = new List<GeneticResultDto>
        {
            new GeneticResultDto
            {
                Summary = "Father is healthy and Mother is affected",
                FatherStatus = "Healthy",
                MotherStatus = "Affected",
                Explanation = "The child inherits one copy of the gene from the sick parent and one healthy copy from the other parent.",
                Advice = new List<string>
                {
                    "Genetic testing for the child after birth.",
                    "Awareness and regular follow-up, even if no symptoms appear."
                }
            },

            new GeneticResultDto
            {
                Summary = "Mother is healthy and Father is a carrier",
                FatherStatus = "Carrier",
                MotherStatus = "Healthy",
                Probabilities = new List<string>
                {
                    "50% chance the child will be a carrier of the mutation.",
                    "50% chance the child will be completely healthy."
                },
                Explanation = "The mutation may or may not be passed on, depending on which gene the child inherits.",
                Advice = new List<string>
                {
                    "Prenatal testing is recommended.",
                    "Consult a genetic doctor for guidance."
                }
            },

            new GeneticResultDto
            {
                Summary = "Father is affected and Mother is a carrier",
                FatherStatus = "Affected",
                MotherStatus = "Carrier",
                Probabilities = new List<string>
                {
                    "50% chance the child will be a carrier of the mutation.",
                    "50% chance the child will be affected."
                },
                Explanation = "The child always receives the mutation, but it may cause disease or only carrier status.",
                Advice = new List<string>
                {
                    "Start early follow-up and monitoring.",
                    "Consult a genetic doctor for guidance."
                }
            },

            // 👇 حالات الشخص نفسه
            new GeneticResultDto
            {
                Summary = "You are healthy",
                Explanation = "No mutation was detected in the CFTR gene. Therefore, the risk is excluded.",
                Advice = new List<string>
                {
                    "Continue routine medical check-ups prior to pregnancy.",
                    "Genetic testing is not required unless there is a documented family history."
                }
            },

            new GeneticResultDto
            {
                Summary = "You have the disease",
                Explanation = "A mutation in the CFTR gene has been detected.",
                Advice = new List<string>
                {
                    "Please consult a genetic doctor for guidance.",
                    "Follow medical and nutrition support through the app."
                }
            },

            new GeneticResultDto
            {
                Summary = "You are a carrier of the mutation",
                Explanation = "Carrying one mutated gene copy does not cause the disease.",
                Advice = new List<string>
                {
                    "Please consult a genetic doctor for guidance.",
                    "Follow medical and nutrition support through the app."
                }
            }
        };

        // ✅ FILTERING (أهم نقطة)
        return testType == TestType.Married
            ? marriedScenarios[random.Next(marriedScenarios.Count)]
            : singleScenarios[random.Next(singleScenarios.Count)];
    }
}