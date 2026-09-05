using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public interface IAIProvider
    {
        ModernizationAnalysisResponse Analyze(
            ModernizationAnalysisRequest request);
    }
}