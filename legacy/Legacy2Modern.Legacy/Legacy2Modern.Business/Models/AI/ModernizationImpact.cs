using System.Collections.Generic;

namespace Legacy2Modern.Business.Models.AI
{
    public class ModernizationImpact
    {
        public string ImpactId { get; set; }

        public string FindingId { get; set; }

        public string ImpactArea { get; set; }

        public string ImpactLevel { get; set; }

        public string Description { get; set; }

        public List<string> AffectedComponents { get; set; }

        public List<string> Risks { get; set; }

        public List<string> Mitigations { get; set; }
    }
}