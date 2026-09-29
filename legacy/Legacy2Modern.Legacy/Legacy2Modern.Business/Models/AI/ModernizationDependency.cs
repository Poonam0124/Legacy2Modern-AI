using System.Collections.Generic;

namespace Legacy2Modern.Business.Models.AI
{
    public class ModernizationDependency
    {
        public string DependencyId { get; set; }

        public string SourceId { get; set; }

        public string TargetId { get; set; }

        public string DependencyType { get; set; }

        public string Description { get; set; }

        public string Impact { get; set; }

        public List<string> AffectedComponents { get; set; }

        public List<string> Reasons { get; set; }
    }
}