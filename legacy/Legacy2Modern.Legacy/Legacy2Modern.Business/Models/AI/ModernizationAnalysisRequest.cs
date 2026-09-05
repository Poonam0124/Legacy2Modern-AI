using System.Collections.Generic;

namespace Legacy2Modern.Business.Models.AI
{
    public class ModernizationAnalysisRequest
    {
        public string ApplicationName { get; set; }

        public string TechnologyStack { get; set; }

        public List<ModernizationFindingExport> Findings { get; set; }
    }
}