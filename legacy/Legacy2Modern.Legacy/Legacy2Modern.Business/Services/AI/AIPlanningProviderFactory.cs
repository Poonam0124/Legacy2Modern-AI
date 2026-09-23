using System;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class AIPlanningProviderFactory
        : IAIPlanningProviderFactory
    {
        private readonly IModernizationPlanningResponseParser _parser;
        private readonly IModernizationPlanningResponseValidator _validator;

        public AIPlanningProviderFactory(
            IModernizationPlanningResponseParser parser,
            IModernizationPlanningResponseValidator validator)
        {
            _parser = parser;
            _validator = validator;
        }

        public IAIPlanningProvider Create(
            AIProviderConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            if (string.IsNullOrWhiteSpace(
                configuration.ProviderName))
            {
                throw new InvalidOperationException(
                    "AI planning provider name is required.");
            }

            switch (configuration.ProviderName.Trim().ToLowerInvariant())
            {
                case "ollama":
                    return new OllamaPlanningAIProvider(
                        configuration,
                        _parser,
                        _validator);

                case "mock":
                    return new MockModernizationPlanningAIProvider();

                default:
                    throw new NotSupportedException(
                        "Unsupported AI planning provider: " +
                        configuration.ProviderName);
            }
        }
    }
}