using System.Collections.Generic;

namespace Legacy2Modern.Business.Models.AI
{
    public class ModernizationDependencyImpactAnalysis
    {
        public string AnalysisTitle { get; set; }

        public string OverallAssessment { get; set; }

        public List<ModernizationDependency> Dependencies { get; set; }

        public List<ModernizationImpact> Impacts { get; set; }
    }
}