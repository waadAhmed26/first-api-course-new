namespace DNAAnalysis.Shared.GeneticResultDtos
{
    public class CreateGeneticResultDto
    {
        public string FatherStatus { get; set; } = default!;
        public string MotherStatus { get; set; } = default!;
        public string? ChildStatus { get; set; }

        public string MessageToPatient { get; set; } = default!;
        public string Advice { get; set; } = default!;
    }
}
