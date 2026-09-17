using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public interface IModernizationAIResponseParser
    {
        ModernizationAnalysisResponse Parse(
            string response);
    }
}