using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public interface IAIProviderOrchestrator
    {
        ModernizationAnalysisResponse Analyze(
            ModernizationAnalysisRequest request);
    }
}