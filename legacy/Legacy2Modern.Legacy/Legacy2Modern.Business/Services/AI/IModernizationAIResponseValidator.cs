using Legacy2Modern.Business.Models;
using Legacy2Modern.Business.Models.AI;
using System.Collections.Generic;

namespace Legacy2Modern.Business.Services.AI
{
    public interface IModernizationAIResponseValidator
    {
        void Validate(
            ModernizationAnalysisResponse response,
            List<ModernizationFindingExport> findings);
    }
}