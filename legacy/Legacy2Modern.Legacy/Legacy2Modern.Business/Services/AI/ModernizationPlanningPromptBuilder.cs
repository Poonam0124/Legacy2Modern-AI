using System;
using System.Text;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationPlanningPromptBuilder
        : IModernizationPlanningPromptBuilder
    {
        public ModernizationPrompt Build(
            ModernizationPlanningContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (context.Findings == null ||
                context.Findings.Count == 0)
            {
                throw new InvalidOperationException(
                    "Modernization findings are required.");
            }

            if (context.Recommendations == null ||
                context.Recommendations.Count == 0)
            {
                throw new InvalidOperationException(
                    "Modernization recommendations are required.");
            }

            var systemInstruction =
                "You are a software modernization planning architect. " +
                "Create a practical, incremental modernization roadmap " +
                "for a legacy application. Prioritize business continuity, " +
                "maintainability, testability, security, scalability, " +
                "and reduction of technical debt.";

            var userInstruction = new StringBuilder();

            userInstruction.AppendLine(
                "Create a modernization implementation plan for the following application.");

            userInstruction.AppendLine();

            userInstruction.AppendLine(
                "Application Name: " + context.ApplicationName);

            userInstruction.AppendLine(
                "Application Description: " +
                context.ApplicationDescription);

            userInstruction.AppendLine(
                "Technology Stack: " +
                context.TechnologyStack);

            userInstruction.AppendLine(
                "Modernization Goal: " +
                context.ModernizationGoal);

            userInstruction.AppendLine();

            userInstruction.AppendLine(
                "Existing Modernization Findings:");

            foreach (var finding in context.Findings)
            {
                userInstruction.AppendLine(
                    "- Finding ID: " + finding.Id);

                userInstruction.AppendLine(
                    "  Title: " + finding.Title);

                userInstruction.AppendLine(
                    "  Description: " + finding.Description);

                userInstruction.AppendLine(
                    "  Risk: " + finding.Risk);

                userInstruction.AppendLine(
                    "  Priority: " + finding.Priority);

                userInstruction.AppendLine();
            }

            userInstruction.AppendLine(
                "Existing AI Modernization Recommendations:");

            foreach (var recommendation in context.Recommendations)
            {
                userInstruction.AppendLine(
                    "- Finding ID: " +
                    recommendation.FindingId);

                userInstruction.AppendLine(
                    "  Recommended Action: " +
                    recommendation.RecommendedAction);

                userInstruction.AppendLine(
                    "  Reasoning: " +
                    recommendation.Reasoning);

                userInstruction.AppendLine(
                    "  Risk: " +
                    recommendation.Risk);

                userInstruction.AppendLine(
                    "  Complexity: " +
                    recommendation.Complexity);

                userInstruction.AppendLine();
            }

            userInstruction.AppendLine(
                "Create an incremental modernization roadmap.");

            userInstruction.AppendLine(
                "Group the work into logical phases.");

            userInstruction.AppendLine(
                "Each phase should have a clear objective and rationale.");

            userInstruction.AppendLine(
                "Each plan item must identify dependencies, " +
                "affected components, implementation steps, " +
                "priority, risk, complexity, and effort.");

            userInstruction.AppendLine(
                "Do not invent application components or findings " +
                "that are not supported by the supplied information.");

            userInstruction.AppendLine(
                "Prefer incremental modernization over a large-scale rewrite.");

            userInstruction.AppendLine(
                "Consider the existing ASP.NET Web Forms application " +
                "and its current technology stack when creating the plan.");
            userInstruction.AppendLine();

            userInstruction.AppendLine(
                "Return the modernization plan as JSON only.");

            userInstruction.AppendLine(
                "Do not return Markdown.");

            userInstruction.AppendLine(
                "Do not use code fences.");

            userInstruction.AppendLine(
                "Do not include explanations before or after the JSON.");

            userInstruction.AppendLine(
                "The response must contain exactly one JSON object.");

            userInstruction.AppendLine(
                "IMPORTANT: The top-level JSON object must contain exactly one property named \"Plan\".");

            userInstruction.AppendLine(
                "The value of \"Plan\" MUST be a JSON object, never an array.");

            userInstruction.AppendLine(
                "Do not put phases directly inside \"Plan\".");

            userInstruction.AppendLine(
                "Do not use \"Plan\" as an array.");

            userInstruction.AppendLine(
                "Do not use alternative structures such as { \"Plan\": [ ... ] }.");

            userInstruction.AppendLine(
                "The \"Plan\" object must contain PlanTitle, OverallStrategy, TargetArchitecture, Phases, and Items.");

            userInstruction.AppendLine(
                "The \"Phases\" property must be an array.");

            userInstruction.AppendLine(
                "The \"Items\" property must be an array.");

            userInstruction.AppendLine(
                "Every item in Items must contain all fields specified in the schema.");

            userInstruction.AppendLine(
                "Use the following JSON structure:");

            userInstruction.AppendLine(@"
                {
                  ""Plan"": {
                    ""PlanTitle"": ""string"",
                    ""OverallStrategy"": ""string"",
                    ""TargetArchitecture"": ""string"",
                    ""Phases"": [
                      {
                        ""PhaseNumber"": 1,
                        ""PhaseName"": ""string"",
                        ""Objective"": ""string"",
                        ""Rationale"": ""string""
                      }
                    ],
                    ""Items"": [
                      {
                        ""PlanItemId"": ""string"",
                        ""PhaseNumber"": 1,
                        ""FindingId"": ""string"",
                        ""Title"": ""string"",
                        ""RecommendedAction"": ""string"",
                        ""Reasoning"": ""string"",
                        ""Priority"": ""string"",
                        ""Risk"": ""string"",
                        ""Complexity"": ""string"",
                        ""Effort"": ""string"",
                        ""Dependencies"": [""string""],
                        ""AffectedComponents"": [""string""],
                        ""ImplementationSteps"": [""string""]
                      }
                    ]
                  }
                }");

            userInstruction.AppendLine(
                "Every plan item must reference one of the supplied Finding IDs.");

            userInstruction.AppendLine(
                "Do not invent Finding IDs.");

            userInstruction.AppendLine(
                "Return valid JSON that can be parsed directly by Newtonsoft.Json.");

            userInstruction.AppendLine(
                "Do not include ProviderName. The application will assign the AI provider.");
            return new ModernizationPrompt
            {
                SystemInstruction = systemInstruction,
                UserInstruction = userInstruction.ToString()
            };
        }
    }
}