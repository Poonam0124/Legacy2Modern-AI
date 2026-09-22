using System.Collections.Generic;

namespace Legacy2Modern.Business.Models.AI
{
    public class ModernizationPlanItem
    {
        public string PlanItemId { get; set; }

        public int PhaseNumber { get; set; }

        public string FindingId { get; set; }

        public string Title { get; set; }

        public string RecommendedAction { get; set; }

        public string Reasoning { get; set; }

        public string Priority { get; set; }

        public string Risk { get; set; }

        public string Complexity { get; set; }

        public string Effort { get; set; }

        public List<string> Dependencies { get; set; }

        public List<string> AffectedComponents { get; set; }

        public List<string> ImplementationSteps { get; set; }
    }
}