namespace Legacy2Modern.Business.Models
{
    public class ModernizationFinding
    {
        public string Id { get; set; }

        public string Category { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Evidence { get; set; }

        public ModernizationRisk Risk { get; set; }

        public string Recommendation { get; set; }

        public ModernizationPriority Priority { get; set; }

        public string AffectedLayer { get; set; }

        public string ModernizationType { get; set; }
        public ModernizationFindingStatus Status { get; set; }

        public ModernizationEffort EstimatedEffort { get; set; }

        public ModernizationStrategy RecommendedStrategy { get; set; }

        public string ExpectedBenefit { get; set; }
    }
}