using Legacy2Modern.Business.Models.AI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationPlanningResponseParser
        : IModernizationPlanningResponseParser
    {
        public ModernizationPlanningResponse Parse(
      string rawResponse)
        {
            if (string.IsNullOrWhiteSpace(rawResponse))
            {
                throw new InvalidOperationException(
                    "AI planning response was empty.");
            }

            var json = rawResponse.Trim();

            if (json.StartsWith("```"))
            {
                var firstNewLine =
                    json.IndexOf('\n');

                if (firstNewLine >= 0)
                {
                    json =
                        json.Substring(
                            firstNewLine + 1);
                }

                var closingFence =
                    json.LastIndexOf("```");

                if (closingFence >= 0)
                {
                    json =
                        json.Substring(
                            0,
                            closingFence);
                }

                json = json.Trim();
            }

            var firstBrace =
                json.IndexOf('{');

            var lastBrace =
                json.LastIndexOf('}');

            if (firstBrace >= 0 &&
                lastBrace > firstBrace)
            {
                json =
                    json.Substring(
                        firstBrace,
                        lastBrace - firstBrace + 1);
            }

            try
            {
                var response =
                    JsonConvert.DeserializeObject
                    <ModernizationPlanningResponse>(
                        json);

                if (response != null &&
                    response.Plan != null)
                {
                    return response;
                }

                var aiPlan =
                    JsonConvert.DeserializeObject
                    <ModernizationPlan>(
                        json);

                if (aiPlan == null)
                {
                    throw new InvalidOperationException(
                        "Unable to parse AI planning response.");
                }

                var normalizedResponse =
                    new ModernizationPlanningResponse
                    {
                        Plan = aiPlan
                    };

                return normalizedResponse;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException(
                    "AI planning response could not be mapped " +
                    "to the expected structure. " +
                    "Raw response: " + json,
                    ex);
            }
        }

        private class AIPlanResponse
        {
            public string PlanTitle { get; set; }

            public string OverallStrategy { get; set; }

            public string TargetArchitecture { get; set; }

            public List<AIPlanPhase> Phases { get; set; }

            public List<AIPlanItem> PlanItems { get; set; }
        }

        private class AIPlanPhase
        {
            public int PhaseNumber { get; set; }

            public string PhaseName { get; set; }

            public string PhaseObjective { get; set; }

            public string PhaseRationale { get; set; }
        }

        private class AIPlanItem
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

    
}