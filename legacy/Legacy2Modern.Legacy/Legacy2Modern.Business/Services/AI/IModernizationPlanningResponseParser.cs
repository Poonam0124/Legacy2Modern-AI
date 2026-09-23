using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public interface IModernizationPlanningResponseParser
    {
        ModernizationPlanningResponse Parse(
            string rawResponse);
    }
}