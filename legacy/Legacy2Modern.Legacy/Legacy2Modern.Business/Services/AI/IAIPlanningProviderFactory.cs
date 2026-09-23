using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public interface IAIPlanningProviderFactory
    {
        IAIPlanningProvider Create(
            AIProviderConfiguration configuration);
    }
}