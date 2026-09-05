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
            _provider = provider;
        }

        public ModernizationAnalysisResponse Analyze(
            ModernizationAnalysisRequest request)
        {
            return _provider.Analyze(request);
        }
    }
}