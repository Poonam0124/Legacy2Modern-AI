using System;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationDependencyImpactRequestBuilder
        : IModernizationDependencyImpactRequestBuilder
    {
        private readonly
            IModernizationDependencyImpactPromptBuilder
            _promptBuilder;

        public ModernizationDependencyImpactRequestBuilder(
            IModernizationDependencyImpactPromptBuilder promptBuilder)
        {
            _promptBuilder = promptBuilder
                ?? throw new ArgumentNullException(
                    "promptBuilder");
        }

        public ModernizationDependencyImpactRequest Build(
            ModernizationDependencyImpactContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (string.IsNullOrWhiteSpace(
                context.ApplicationName))
            {
                throw new InvalidOperationException(
                    "Application name is required.");
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

            var prompt =
                _promptBuilder.Build(context);

            return new ModernizationDependencyImpactRequest
            {
                ApplicationName =
                    context.ApplicationName,

                ApplicationDescription =
                    context.ApplicationDescription,

                TechnologyStack =
                    context.TechnologyStack,

                ModernizationGoal =
                    context.ModernizationGoal,

                Prompt = prompt
            };
        }
    }
}