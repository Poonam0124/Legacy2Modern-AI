using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationAIService : IModernizationAIService
    {
        private readonly IAIProviderOrchestrator _orchestrator;

        public ModernizationAIService(
            IAIProviderOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        public ModernizationAnalysisResponse Analyze(
            ModernizationAnalysisRequest request)
        {
            return _orchestrator.Analyze(request);
        }
    }
}