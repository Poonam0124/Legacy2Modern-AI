using System.Collections.Generic;

namespace Legacy2Modern.Business.Models.AI
{
    public class ModernizationPlan
    {
        public string PlanTitle { get; set; }

        public string OverallStrategy { get; set; }

        public string TargetArchitecture { get; set; }

        public List<ModernizationPlanPhase> Phases { get; set; }

        public List<ModernizationPlanItem> Items { get; set; }
    }
}