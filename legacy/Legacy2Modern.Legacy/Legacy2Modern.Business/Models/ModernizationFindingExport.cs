namespace Legacy2Modern.Business.Models
{
    public class ModernizationFindingExport
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Evidence { get; set; }
        public string Risk { get; set; }
        public string Recommendation { get; set; }
        public string Priority { get; set; }
        public string AffectedLayer { get; set; }
        public string ModernizationType { get; set; }
        public string Status { get; set; }
        public string EstimatedEffort { get; set; }

        public string RecommendedStrategy { get; set; }

        public string ExpectedBenefit { get; set; }
    }
}