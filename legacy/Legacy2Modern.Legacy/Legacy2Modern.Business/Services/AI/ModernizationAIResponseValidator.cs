using Legacy2Modern.Business.Models;
using Legacy2Modern.Business.Models.AI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationAIResponseValidator
        : IModernizationAIResponseValidator
    {
        public void Validate(
            ModernizationAnalysisResponse response,
            List<ModernizationFindingExport> findings)
        {
            if (response == null)
            {
                throw new ArgumentException(
                    "AI analysis response is required.");
            }

            if (string.IsNullOrWhiteSpace(
                response.OverallAssessment))
            {
                throw new ArgumentException(
                    "AI response is missing OverallAssessment.");
            }

            if (string.IsNullOrWhiteSpace(
                response.RecommendedApproach))
            {
                throw new ArgumentException(
                    "AI response is missing RecommendedApproach.");
            }

            if (string.IsNullOrWhiteSpace(
                response.TargetArchitecture))
            {
                throw new ArgumentException(
                    "AI response is missing TargetArchitecture.");
            }

            if (response.Recommendations == null ||
                response.Recommendations.Count == 0)
            {
                throw new ArgumentException(
                    "AI response contains no recommendations.");
            }

            if (findings == null || findings.Count == 0)
            {
                throw new ArgumentException(
                    "Modernization findings are required for validation.");
            }

            var findingIds =
                new HashSet<string>(
                    findings
                        .Where(x => !string.IsNullOrWhiteSpace(x.Id))
                        .Select(x => x.Id),
                    StringComparer.OrdinalIgnoreCase);

            var recommendationIds =
                new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase);

            foreach (var recommendation
                in response.Recommendations)
            {
                if (recommendation == null)
                {
                    throw new ArgumentException(
                        "AI response contains an empty recommendation.");
                }

                if (string.IsNullOrWhiteSpace(
                    recommendation.FindingId))
                {
                    throw new ArgumentException(
                        "AI recommendation is missing FindingId.");
                }

                if (!findingIds.Contains(
                    recommendation.FindingId))
                {
                    throw new ArgumentException(
                        "AI recommendation references an unknown FindingId: " +
                        recommendation.FindingId);
                }

                if (!recommendationIds.Add(
                    recommendation.FindingId))
                {
                    throw new ArgumentException(
                        "AI response contains duplicate recommendation for FindingId: " +
                        recommendation.FindingId);
                }

                if (string.IsNullOrWhiteSpace(
                    recommendation.RecommendedAction))
                {
                    throw new ArgumentException(
                        "AI recommendation is missing RecommendedAction for FindingId: " +
                        recommendation.FindingId);
                }

                if (string.IsNullOrWhiteSpace(
                    recommendation.Reasoning))
                {
                    throw new ArgumentException(
                        "AI recommendation is missing Reasoning for FindingId: " +
                        recommendation.FindingId);
                }

                if (recommendation.Confidence < 0 ||
                    recommendation.Confidence > 1)
                {
                    throw new ArgumentException(
                        "AI recommendation Confidence must be between 0 and 1 for FindingId: " +
                        recommendation.FindingId);
                }

                if (string.IsNullOrWhiteSpace(
                    recommendation.Risk))
                {
                    throw new ArgumentException(
                        "AI recommendation is missing Risk for FindingId: " +
                        recommendation.FindingId);
                }

                if (string.IsNullOrWhiteSpace(
                    recommendation.Complexity))
                {
                    throw new ArgumentException(
                        "AI recommendation is missing Complexity for FindingId: " +
                        recommendation.FindingId);
                }
            }

            if (recommendationIds.Count != findingIds.Count)
            {
                throw new ArgumentException(
                    "AI response does not contain exactly one recommendation for each modernization finding.");
            }
        }
    }
}