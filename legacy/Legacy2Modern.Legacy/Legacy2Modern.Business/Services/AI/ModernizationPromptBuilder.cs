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

            builder.AppendLine();
            builder.AppendLine("Response Format:");
            builder.AppendLine(
                "Return the result as JSON only.");
            builder.AppendLine(
                "Do not use Markdown.");
            builder.AppendLine(
                "Do not use code fences.");
            builder.AppendLine(
                "Do not add explanations before or after the JSON.");

            builder.AppendLine();
            builder.AppendLine(
                "The JSON must follow this exact structure:");

            builder.AppendLine("{");
            builder.AppendLine(
                "  \"OverallAssessment\": \"string\",");
            builder.AppendLine(
                "  \"RecommendedApproach\": \"string\",");
            builder.AppendLine(
                "  \"TargetArchitecture\": \"string\",");
            builder.AppendLine(
                "  \"Recommendations\": [");
            builder.AppendLine("    {");
            builder.AppendLine(
                "      \"FindingId\": \"string\",");
            builder.AppendLine(
                "      \"RecommendedAction\": \"string\",");
            builder.AppendLine(
                "      \"Reasoning\": \"string\",");
            builder.AppendLine(
                "      \"AffectedComponents\": [");
            builder.AppendLine(
                "        \"string\"");
            builder.AppendLine(
                "      ],");
            builder.AppendLine(
                "      \"ImplementationSteps\": [");
            builder.AppendLine(
                "        \"string\"");
            builder.AppendLine(
                "      ],");
            builder.AppendLine(
                "      \"Risk\": \"Low|Medium|High|Critical\",");
            builder.AppendLine(
                "      \"Complexity\": \"Low|Medium|High|VeryHigh\",");
            builder.AppendLine(
                "      \"Confidence\": 0.0");
            builder.AppendLine("    }");
            builder.AppendLine("  ]");
            builder.AppendLine("}");

            builder.AppendLine();
            builder.AppendLine(
                "Additional rules:");

            builder.AppendLine(
                "- Include one recommendation for each modernization finding provided.");

            builder.AppendLine(
                "- FindingId must match an existing finding Id exactly.");

            builder.AppendLine(
                "- Confidence must be a number between 0 and 1.");

            builder.AppendLine(
                "- Do not invent findings that are not provided.");

            builder.AppendLine(
                "- Base recommendations only on the supplied application information and findings.");

            builder.AppendLine(
                "- Prefer incremental modernization over unnecessary full rewrites.");

            return new ModernizationPrompt
            {
                SystemInstruction = systemInstruction,
                UserInstruction = builder.ToString()
            };
        }
    }
}