using System;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationAIRequestBuilder
        : IModernizationAIRequestBuilder
    {
        private readonly IModernizationPromptBuilder _promptBuilder;

        public ModernizationAIRequestBuilder(
            IModernizationPromptBuilder promptBuilder)
        {
            _promptBuilder = promptBuilder;
        }

        public ModernizationAnalysisRequest Build(
    ModernizationAnalysisContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (context.Findings == null)
                throw new ArgumentException(
                    "Modernization findings are required.");

            var prompt =
                _promptBuilder.Build(context);

            return new ModernizationAnalysisRequest
            {
                ApplicationName =
                    context.ApplicationName,

                TechnologyStack =
                    context.TechnologyStack,

                Findings =
                    context.Findings,

                Prompt =
                    prompt
            };
        }
    }
}