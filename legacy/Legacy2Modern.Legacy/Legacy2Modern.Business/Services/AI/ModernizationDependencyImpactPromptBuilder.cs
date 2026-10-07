using System;
using System.Text;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationDependencyImpactPromptBuilder
        : IModernizationDependencyImpactPromptBuilder
    {
        public ModernizationPrompt Build(
            ModernizationDependencyImpactContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

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

            if (context.Plan == null)
            {
                throw new InvalidOperationException(
                    "Modernization plan is required.");
            }

            var systemInstruction =
                "You are a software modernization dependency and impact " +
                "analysis architect. Analyze a legacy application's " +
                "modernization findings, recommendations, and execution " +
                "plan. Identify meaningful dependencies between " +
                "modernization activities and assess their potential impact " +
                "on application components. Be conservative and do not " +
                "invent unsupported dependencies or components.";

            var userInstruction =
                new StringBuilder();

            userInstruction.AppendLine(
                "Analyze the modernization dependencies and impacts " +
                "for the following application.");
            userInstruction.AppendLine();

            userInstruction.AppendLine(
                "Application:");
            userInstruction.AppendLine(
                "Name: " + context.ApplicationName);
            userInstruction.AppendLine(
                "Description: " + context.ApplicationDescription);
            userInstruction.AppendLine(
                "Technology Stack: " + context.TechnologyStack);
            userInstruction.AppendLine(
                "Modernization Goal: " + context.ModernizationGoal);
            userInstruction.AppendLine();

            userInstruction.AppendLine(
                "Modernization Findings:");

            foreach (var finding in context.Findings)
            {
                userInstruction.AppendLine(
                    "- Finding ID: " + finding.Id);
                userInstruction.AppendLine(
                    "  Title: " + finding.Title);
                userInstruction.AppendLine(
                    "  Description: " + finding.Description);
                userInstruction.AppendLine(
                    "  Category: " + finding.Category);
                userInstruction.AppendLine(
                    "  Risk: " + finding.Risk);
                userInstruction.AppendLine(
                    "  Recommendation: " + finding.Recommendation);
                userInstruction.AppendLine(
                    "  Priority: " + finding.Priority);
                userInstruction.AppendLine(
                    "  Affected Layer: " + finding.AffectedLayer);
                userInstruction.AppendLine(
                    "  Modernization Type: " + finding.ModernizationType);
                userInstruction.AppendLine(
                    "  Status: " + finding.Status);
                userInstruction.AppendLine(
                    "  Estimated Effort: " + finding.EstimatedEffort);
            }

            userInstruction.AppendLine();

            userInstruction.AppendLine(
                "Modernization Recommendations:");

            foreach (var recommendation in context.Recommendations)
            {
                userInstruction.AppendLine(
                    "- Finding ID: " + recommendation.FindingId);
                userInstruction.AppendLine(
                    "  Recommended Action: " +
                    recommendation.RecommendedAction);
                userInstruction.AppendLine(
                    "  Reasoning: " +
                    recommendation.Reasoning);
                userInstruction.AppendLine(
                    "  Risk: " + recommendation.Risk);
                userInstruction.AppendLine(
                    "  Complexity: " + recommendation.Complexity);
            }

            userInstruction.AppendLine();

            userInstruction.AppendLine(
                "Modernization Plan:");

            userInstruction.AppendLine(
                "Plan Title: " + context.Plan.PlanTitle);
            userInstruction.AppendLine(
                "Overall Strategy: " +
                context.Plan.OverallStrategy);
            userInstruction.AppendLine(
                "Target Architecture: " +
                context.Plan.TargetArchitecture);

            userInstruction.AppendLine();

            userInstruction.AppendLine(
                "Plan Phases:");

            foreach (var phase in context.Plan.Phases)
            {
                userInstruction.AppendLine(
                    "- Phase " + phase.PhaseNumber +
                    ": " + phase.PhaseName);
                userInstruction.AppendLine(
                    "  Objective: " + phase.Objective);
                userInstruction.AppendLine(
                    "  Rationale: " + phase.Rationale);
            }

            userInstruction.AppendLine();

            userInstruction.AppendLine(
                "Plan Items:");

            foreach (var item in context.Plan.Items)
            {
                userInstruction.AppendLine(
                    "- Plan Item ID: " + item.PlanItemId);
                userInstruction.AppendLine(
                    "  Phase Number: " + item.PhaseNumber);
                userInstruction.AppendLine(
                    "  Finding ID: " + item.FindingId);
                userInstruction.AppendLine(
                    "  Title: " + item.Title);
                userInstruction.AppendLine(
                    "  Recommended Action: " +
                    item.RecommendedAction);
                userInstruction.AppendLine(
                    "  Dependencies: " +
                    string.Join(", ", item.Dependencies ??
                        new System.Collections.Generic.List<string>()));
                userInstruction.AppendLine(
                    "  Affected Components: " +
                    string.Join(", ", item.AffectedComponents ??
                        new System.Collections.Generic.List<string>()));
            }

            userInstruction.AppendLine();

            userInstruction.AppendLine(
                "Dependency analysis requirements:");

            userInstruction.AppendLine(
                "1. Identify meaningful dependencies between " +
                "modernization findings or plan items.");

            userInstruction.AppendLine(
                "2. Identify which modernization activity should occur " +
                "before another activity when a dependency exists.");

            userInstruction.AppendLine(
                "3. Explain why the dependency exists.");

            userInstruction.AppendLine(
                "4. Identify the impact of each dependency.");

            userInstruction.AppendLine(
                "5. Do not create dependencies merely because two " +
                "activities are related.");

            userInstruction.AppendLine(
                "6. Use only Finding IDs and Plan Item IDs supplied " +
                "in the input.");

            userInstruction.AppendLine(
                "7. Do not invent application components.");

            userInstruction.AppendLine();

            userInstruction.AppendLine(
                "Impact analysis requirements:");

            userInstruction.AppendLine(
                "1. Identify the affected application components.");

            userInstruction.AppendLine(
                "2. Identify the impact area.");

            userInstruction.AppendLine(
                "3. Classify the impact level.");

            userInstruction.AppendLine(
                "4. Identify realistic risks.");

            userInstruction.AppendLine(
                "5. Provide practical mitigation strategies.");

            userInstruction.AppendLine(
                "6. Base the analysis only on the supplied " +
                "application information.");

            userInstruction.AppendLine();

            userInstruction.AppendLine(
                "Return the analysis as JSON only.");
            userInstruction.AppendLine(
                "Do not return Markdown.");
            userInstruction.AppendLine(
                "Do not use code fences.");
            userInstruction.AppendLine(
                "Do not include explanations before or after the JSON.");

            return new ModernizationPrompt
            {
                SystemInstruction = systemInstruction,
                UserInstruction = userInstruction.ToString()
            };
        }
    }
}