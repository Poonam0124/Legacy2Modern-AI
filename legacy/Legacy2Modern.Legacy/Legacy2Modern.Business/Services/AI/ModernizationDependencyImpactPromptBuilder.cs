
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

            if (context.Plan.Items == null ||
                context.Plan.Items.Count == 0)
            {
                throw new InvalidOperationException(
                    "Modernization plan items are required.");
            }

            var systemInstruction =
                "You are a conservative software modernization " +
                "dependency and impact analysis architect. Analyze only " +
                "the supplied findings, recommendations, plan items, " +
                "phases, and application information. Evidence and " +
                "traceability are more important than producing many " +
                "dependencies. Never invent findings, plan items, " +
                "components, configuration changes, or architectural " +
                "decisions. If evidence is insufficient, omit the " +
                "dependency rather than speculate.";

            var userInstruction = new StringBuilder();

            userInstruction.AppendLine(
                "Analyze the dependencies and impacts of the supplied " +
                "modernization activities.");
            userInstruction.AppendLine();

            userInstruction.AppendLine("APPLICATION");
            userInstruction.AppendLine(
                "Name: " + context.ApplicationName);
            userInstruction.AppendLine(
                "Description: " + context.ApplicationDescription);
            userInstruction.AppendLine(
                "Technology Stack: " + context.TechnologyStack);
            userInstruction.AppendLine(
                "Modernization Goal: " + context.ModernizationGoal);
            userInstruction.AppendLine();

            userInstruction.AppendLine("MODERNIZATION FINDINGS");

            foreach (var finding in context.Findings)
            {
                userInstruction.AppendLine(
                    "- Finding ID: " + finding.Id);
                userInstruction.AppendLine(
                    "  Title: " + finding.Title);
                userInstruction.AppendLine(
                    "  Description: " + finding.Description);
                userInstruction.AppendLine(
                    "  Evidence: " + finding.Evidence);
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
            userInstruction.AppendLine("AI RECOMMENDATIONS");

            foreach (var recommendation in context.Recommendations)
            {
                userInstruction.AppendLine(
                    "- Finding ID: " + recommendation.FindingId);
                userInstruction.AppendLine(
                    "  Recommended Action: " +
                    recommendation.RecommendedAction);
                userInstruction.AppendLine(
                    "  Reasoning: " + recommendation.Reasoning);
                userInstruction.AppendLine(
                    "  Risk: " + recommendation.Risk);
                userInstruction.AppendLine(
                    "  Complexity: " + recommendation.Complexity);
            }

            userInstruction.AppendLine();
            userInstruction.AppendLine("MODERNIZATION PLAN");

            userInstruction.AppendLine(
                "Plan Title: " + context.Plan.PlanTitle);
            userInstruction.AppendLine(
                "Overall Strategy: " + context.Plan.OverallStrategy);
            userInstruction.AppendLine(
                "Target Architecture: " +
                context.Plan.TargetArchitecture);

            userInstruction.AppendLine();
            userInstruction.AppendLine("PLAN PHASES");

            if (context.Plan.Phases != null)
            {
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
            }

            userInstruction.AppendLine();
            userInstruction.AppendLine("PLAN ITEMS");

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
                    "  Recommended Action: " + item.RecommendedAction);
                userInstruction.AppendLine(
                    "  Dependencies: " +
                    string.Join(", ", item.Dependencies ??
                        new System.Collections.Generic.List<string>()));
                userInstruction.AppendLine(
                    "  Affected Components: " +
                    string.Join(", ", item.AffectedComponents ??
                        new System.Collections.Generic.List<string>()));
                userInstruction.AppendLine(
                    "  Implementation Steps: " +
                    string.Join(", ", item.ImplementationSteps ??
                        new System.Collections.Generic.List<string>()));
            }

            userInstruction.AppendLine();
            userInstruction.AppendLine("DEPENDENCY REQUIREMENTS");

            userInstruction.AppendLine(
                "1. SourceId must identify the prerequisite plan item.");
            userInstruction.AppendLine(
                "2. TargetId must identify the plan item that depends " +
                "on that prerequisite.");
            userInstruction.AppendLine(
                "3. Both SourceId and TargetId must exactly match " +
                "Plan Item IDs listed above.");
            userInstruction.AppendLine(
                "4. Never use Finding IDs, phase IDs, milestone labels, " +
                "or invented IDs as dependency endpoints.");
            userInstruction.AppendLine(
                "5. Explain the specific evidence showing why the " +
                "target activity depends on the source activity.");
            userInstruction.AppendLine(
                "6. Existing item dependencies and phase ordering may " +
                "provide evidence, but do not assume a dependency " +
                "exists merely because one item appears earlier.");
            userInstruction.AppendLine(
                "7. Do not reverse prerequisite direction.");
            userInstruction.AppendLine(
                "8. SourceId and TargetId must always be different. " +
                "Never create a dependency from a plan item to itself.");
            userInstruction.AppendLine(
                "9. Do not create circular dependencies.");
            userInstruction.AppendLine(
                "10. Do not invent prerequisites such as migrating to " +
                "appsettings.json, introducing a new service, or " +
                "changing configuration unless explicitly supported " +
                "by the supplied findings or plan.");
            userInstruction.AppendLine(
                "11. If no meaningful dependency is supported, return " +
                "an empty Dependencies array.");
            userInstruction.AppendLine(
                "12. Keep descriptions specific to the actual activities.");
            userInstruction.AppendLine();

            userInstruction.AppendLine("IMPACT REQUIREMENTS");

            userInstruction.AppendLine(
                "1. FindingId must exactly match an ID from the " +
                "supplied Modernization Findings.");
            userInstruction.AppendLine(
                "2. Identify a concrete impact on the application, " +
                "its behavior, maintainability, reliability, security, " +
                "or delivery.");
            userInstruction.AppendLine(
                "3. AffectedComponents must contain only components " +
                "supported by the supplied evidence.");
            userInstruction.AppendLine(
                "4. Risks must describe plausible failure modes or " +
                "negative consequences, not generic statements.");
            userInstruction.AppendLine(
                "5. Mitigations must explain practical actions that " +
                "reduce the identified risks.");
            userInstruction.AppendLine(
                "6. Explain why the impact level is appropriate.");
            userInstruction.AppendLine(
                "7. Avoid repetitive or generic impacts. Each impact " +
                "must provide distinct, useful information.");
            userInstruction.AppendLine(
                "8. Do not claim that an implementation already exists " +
                "unless the supplied context establishes that fact.");
            userInstruction.AppendLine();

            userInstruction.AppendLine("OUTPUT RULES");

            userInstruction.AppendLine(
                "Return one JSON object matching the required schema.");
            userInstruction.AppendLine(
                "Include AnalysisTitle, OverallAssessment, Dependencies, " +
                "and Impacts.");
            userInstruction.AppendLine(
                "Every dependency must include DependencyId, SourceId, " +
                "TargetId, DependencyType, Description, Impact, " +
                "AffectedComponents, and Reasons.");
            userInstruction.AppendLine(
                "Every impact must include ImpactId, FindingId, " +
                "ImpactArea, ImpactLevel, Description, " +
                "AffectedComponents, Risks, and Mitigations.");
            userInstruction.AppendLine(
                "Return JSON only. Do not include Markdown, code fences, " +
                "or explanatory text outside the JSON.");

            return new ModernizationPrompt
            {
                SystemInstruction = systemInstruction,
                UserInstruction = userInstruction.ToString()
            };
        }
    }
}
