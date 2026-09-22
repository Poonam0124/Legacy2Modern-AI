using System;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationPlanningRequestBuilder
        : IModernizationPlanningRequestBuilder
    {
        private readonly IModernizationPlanningPromptBuilder _promptBuilder;

        public ModernizationPlanningRequestBuilder(
            IModernizationPlanningPromptBuilder promptBuilder)
        {
            _promptBuilder = promptBuilder;
        }

        public ModernizationPlanningRequest Build(
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

            var prompt = _promptBuilder.Build(context);

            return new ModernizationPlanningRequest
            {
                ApplicationName = context.ApplicationName,
                ApplicationDescription =
                    context.ApplicationDescription,
                TechnologyStack =
                    context.TechnologyStack,
                ModernizationGoal =
                    context.ModernizationGoal,
                Findings = context.Findings,
                Recommendations = context.Recommendations,
                Prompt = prompt
            };
        }
    }
}