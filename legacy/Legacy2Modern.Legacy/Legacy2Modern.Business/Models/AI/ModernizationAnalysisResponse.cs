using System.Collections.Generic;

namespace Legacy2Modern.Business.Models.AI
{
    public class ModernizationAnalysisResponse
    {
        public string OverallAssessment { get; set; }

        public string RecommendedApproach { get; set; }

        public string TargetArchitecture { get; set; }

        public List<ModernizationRecommendation> Recommendations { get; set; }
    }
}