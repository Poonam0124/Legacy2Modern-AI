using System;
using System.Collections.Generic;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class MockModernizationAIService
        : IAIProvider,
          IAIProviderFallback
    {
        public ModernizationAnalysisResponse Analyze(
            ModernizationAnalysisRequest request)
        {
            return new ModernizationAnalysisResponse
            {
                OverallAssessment =
                    "Mock AI analysis completed successfully.",

                RecommendedApproach =
                    "Use incremental modernization to reduce risk.",

                TargetArchitecture =
                    "ASP.NET Core Web API with a modern frontend and SQL Server.",

                Recommendations =
                    new List<ModernizationRecommendation>(),

                ProviderName = "Mock (Fallback)"
            };
        }
    }
}