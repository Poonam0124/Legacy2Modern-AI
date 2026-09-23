using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public interface IModernizationPlanningResponseValidator
    {
        ModernizationPlanValidationResult Validate(
            ModernizationPlanningResponse response);
    }
}