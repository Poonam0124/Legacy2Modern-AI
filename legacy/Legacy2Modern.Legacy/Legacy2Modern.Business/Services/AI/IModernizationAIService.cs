using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public interface IModernizationAIService
    {
        ModernizationAnalysisResponse Analyze(
            ModernizationAnalysisRequest request);
    }
}