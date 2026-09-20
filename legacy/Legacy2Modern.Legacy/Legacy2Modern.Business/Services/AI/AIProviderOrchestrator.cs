using System;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class AIProviderOrchestrator
        : IAIProviderOrchestrator
    {
        private readonly IAIProvider _primaryProvider;
        private readonly IAIProviderFallback _fallbackProvider;
        private readonly AIFallbackConfiguration _fallbackConfiguration;

        public AIProviderOrchestrator(
            IAIProvider primaryProvider,
            IAIProviderFallback fallbackProvider,
            AIFallbackConfiguration fallbackConfiguration)
        {
            if (primaryProvider == null)
                throw new ArgumentNullException("primaryProvider");

            if (fallbackProvider == null)
                throw new ArgumentNullException("fallbackProvider");

            if (fallbackConfiguration == null)
                throw new ArgumentNullException("fallbackConfiguration");

            _primaryProvider = primaryProvider;
            _fallbackProvider = fallbackProvider;
            _fallbackConfiguration = fallbackConfiguration;
        }

        public ModernizationAnalysisResponse Analyze(
            ModernizationAnalysisRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            try
            {
                return _primaryProvider.Analyze(request);
            }
            catch (Exception primaryException)
            {
                if (!_fallbackConfiguration.Enabled)
                    throw;

                try
                {
                    return _fallbackProvider.Analyze(request);
                }
                catch (Exception fallbackException)
                {
                    throw new InvalidOperationException(
                        "Both the primary AI provider and fallback AI provider failed.",
                        new AggregateException(
                            "AI provider execution failed.",
                            primaryException,
                            fallbackException));
                }
            }
        }
    }
}