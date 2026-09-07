using System;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationAIService
        : IModernizationAIService
    {
        private readonly IAIProvider _provider;

        public ModernizationAIService(
            IAIProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            _provider = provider;
        }

        public ModernizationAnalysisResponse Analyze(
            ModernizationAnalysisRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            return _provider.Analyze(request);
        }
    }
}