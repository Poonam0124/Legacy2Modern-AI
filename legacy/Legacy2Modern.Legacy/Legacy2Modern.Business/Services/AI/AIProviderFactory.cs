using System;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class AIProviderFactory
        : IAIProviderFactory
    {
        public IAIProvider Create(
            AIProviderConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(
                    "configuration");

            if (string.IsNullOrWhiteSpace(
                configuration.ProviderName))
            {
                throw new ArgumentException(
                    "AI provider name is required.");
            }

            if (string.Equals(
                configuration.ProviderName,
                "Ollama",
                StringComparison.OrdinalIgnoreCase))
            {
                return new OllamaAIProvider(
                    configuration.Endpoint,
                    configuration.ModelName,
                    configuration.TimeoutSeconds);
            }

            throw new NotSupportedException(
                "AI provider '" +
                configuration.ProviderName +
                "' is not supported.");
        }
    }
}