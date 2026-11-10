
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
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.Findings == null ||
                request.Findings.Count == 0)
            {
                throw new InvalidOperationException(
                    "Mock fallback requires modernization findings.");
            }

            var recommendations =
                new List<ModernizationRecommendation>();

            foreach (var finding in request.Findings)
            {
                if (finding == null ||
                    string.IsNullOrWhiteSpace(finding.Id))
                {
                    throw new InvalidOperationException(
                        "Mock fallback received a finding without an ID.");
                }

                var recommendation =
                    new ModernizationRecommendation
                    {
                        FindingId = finding.Id,

                        RecommendedAction =
                            "Review and modernize the affected component incrementally.",

                        Reasoning =
                            "Incremental modernization allows existing behavior to be verified before proceeding.",

                        Risk =
                            "Existing behavior may change during refactoring.",

                        Complexity = "Medium",

                        Confidence = 0.75m,

                        AffectedComponents =
                            new List<string>
                            {
                                finding.AffectedLayer ??
                                    "Unspecified component"
                            },

                        ImplementationSteps =
                            new List<string>
                            {
                                "Review the existing implementation.",
                                "Implement the change in a small, isolated step.",
                                "Run regression tests and verify the result."
                            }
                    };

                recommendations.Add(recommendation);
            }

            return new ModernizationAnalysisResponse
            {
                OverallAssessment =
                    "Mock AI analysis completed successfully.",

                RecommendedApproach =
                    "Use incremental modernization to reduce risk.",

                TargetArchitecture =
                    "ASP.NET Core Web API with a modern frontend and SQL Server.",

                Recommendations = recommendations,

                ProviderName = "Mock (Fallback)"
            };
        }
    }
}
