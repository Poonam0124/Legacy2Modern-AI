using System.Collections.Generic;

namespace Legacy2Modern.Business.Models.AI
{
    public class ModernizationAnalysisContext
    {
        public string ApplicationName { get; set; }

        public string ApplicationDescription { get; set; }

        public string TechnologyStack { get; set; }

        public string ModernizationGoal { get; set; }

        public List<ModernizationFindingExport> Findings { get; set; }
    }
}