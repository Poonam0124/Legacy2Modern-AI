using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public interface IModernizationDependencyImpactAnalysisService
    {
        ModernizationDependencyImpactAnalysis Analyze(
            ModernizationDependencyImpactContext context);
    }
}