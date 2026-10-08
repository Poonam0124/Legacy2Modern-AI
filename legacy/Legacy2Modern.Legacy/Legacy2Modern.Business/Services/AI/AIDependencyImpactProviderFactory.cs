using System;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class AIDependencyImpactProviderFactory
        : IAIDependencyImpactProviderFactory
    {
        private readonly AIProviderConfiguration _configuration;
        private readonly IModernizationDependencyImpactResponseParser _parser;
        private readonly IModernizationDependencyImpactResponseValidator _validator;

        public AIDependencyImpactProviderFactory(
            AIProviderConfiguration configuration,
            IModernizationDependencyImpactResponseParser parser,
            IModernizationDependencyImpactResponseValidator validator)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException("configuration");

            _parser = parser
                ?? throw new ArgumentNullException("parser");

            _validator = validator
                ?? throw new ArgumentNullException("validator");
        }

        public IAIDependencyImpactProvider Create()
        {
            if (string.IsNullOrWhiteSpace(
                _configuration.ProviderName))
            {
                throw new InvalidOperationException(
                    "AI provider name is not configured.");
            }

            switch (_configuration.ProviderName
                .Trim()
                .ToLowerInvariant())
            {
                case "ollama":
                    return new OllamaDependencyImpactProvider(
                        _configuration,
                        _parser,
                        _validator);

                default:
                    throw new InvalidOperationException(
                        "Unsupported AI dependency and impact provider: " +
                        _configuration.ProviderName);
            }
        }
    }
}