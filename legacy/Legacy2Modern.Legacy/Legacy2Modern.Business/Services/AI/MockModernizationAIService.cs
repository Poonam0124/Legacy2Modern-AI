using System.Collections.Generic;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class MockModernizationAIService
        : IModernizationAIService
    {
        public ModernizationAnalysisResponse Analyze(
            ModernizationAnalysisRequest request)
        {
            return new ModernizationAnalysisResponse
            {
                OverallAssessment =
                    "Modernization assessment is ready for AI analysis.",

                RecommendedApproach =
                    "Incremental modernization",

                TargetArchitecture =
                    "Layered application with clearly separated responsibilities.",

                Recommendations =
                    new List<ModernizationRecommendation>()
            };
        }
    }
}