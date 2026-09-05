using System;
using System.Text;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationPromptBuilder
        : IModernizationPromptBuilder
    {
        public ModernizationPrompt Build(
            ModernizationAnalysisContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var systemInstruction =
                "You are a software modernization architect. " +
                "Analyze legacy applications and provide practical, " +
                "incremental modernization recommendations. " +
                "Prioritize maintainability, testability, scalability, " +
                "security, and reduced technical debt.";

            var builder = new StringBuilder();

            builder.AppendLine(
                "Analyze the following legacy application:");

            builder.AppendLine();

            builder.AppendLine(
                "Application Name: " +
                context.ApplicationName);

            builder.AppendLine(
                "Application Description: " +
                context.ApplicationDescription);

            builder.AppendLine(
                "Technology Stack: " +
                context.TechnologyStack);

            builder.AppendLine(
                "Modernization Goal: " +
                context.ModernizationGoal);

            builder.AppendLine();

            builder.AppendLine("Modernization Findings:");

            if (context.Findings != null)
            {
                foreach (var finding in context.Findings)
                {
                    builder.AppendLine();
                    builder.AppendLine(
                        "Finding ID: " + finding.Id);

                    builder.AppendLine(
                        "Category: " + finding.Category);

                    builder.AppendLine(
                        "Title: " + finding.Title);

                    builder.AppendLine(
                        "Description: " +
                        finding.Description);

                    builder.AppendLine(
                        "Evidence: " +
                        finding.Evidence);

                    builder.AppendLine(
                        "Risk: " +
                        finding.Risk);

                    builder.AppendLine(
                        "Priority: " +
                        finding.Priority);

                    builder.AppendLine(
                        "Affected Layer: " +
                        finding.AffectedLayer);

                    builder.AppendLine(
                        "Modernization Type: " +
                        finding.ModernizationType);

                    builder.AppendLine(
                        "Recommended Strategy: " +
                        finding.RecommendedStrategy);

                    builder.AppendLine(
                        "Expected Benefit: " +
                        finding.ExpectedBenefit);

                    builder.AppendLine(
                        "Estimated Effort: " +
                        finding.EstimatedEffort);
                }
            }

            builder.AppendLine();
            builder.AppendLine(
                "Provide an overall modernization assessment.");

            builder.AppendLine(
                "Recommend an incremental modernization approach.");

            builder.AppendLine(
                "Identify the most important modernization priorities.");

            builder.AppendLine(
                "For each finding, provide practical implementation steps.");

            builder.AppendLine(
                "Consider the existing technology before recommending " +
                "a replacement.");

            return new ModernizationPrompt
            {
                SystemInstruction = systemInstruction,
                UserInstruction = builder.ToString()
            };
        }
    }
}