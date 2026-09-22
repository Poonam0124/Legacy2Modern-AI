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

            return new ModernizationPrompt
            {
                SystemInstruction = systemInstruction,
                UserInstruction = userInstruction.ToString()
            };
        }
    }
}