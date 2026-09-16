using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public interface IAIProviderFactory
    {
        IAIProvider Create(
            AIProviderConfiguration configuration);
    }
}