using System.Collections.Generic;

namespace Legacy2Modern.Business.Models.AI
{
    public class ModernizationRecommendation
    {
        public string FindingId { get; set; }

        public string RecommendedAction { get; set; }

        public string Reasoning { get; set; }

        public List<string> AffectedComponents { get; set; }

        public List<string> ImplementationSteps { get; set; }

        public string Risk { get; set; }

        public string Complexity { get; set; }

        public decimal Confidence { get; set; }
    }
}