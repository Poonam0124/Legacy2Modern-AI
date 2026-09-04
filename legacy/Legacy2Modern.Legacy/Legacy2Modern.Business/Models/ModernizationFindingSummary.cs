namespace Legacy2Modern.Business.Models
{
    public class ModernizationFindingSummary
    {
        public int TotalFindings { get; set; }

        public int HighOrCriticalRiskCount { get; set; }

        public int HighOrCriticalPriorityCount { get; set; }

        public int IdentifiedCount { get; set; }

        public int LowEffortCount { get; set; }

        public int MediumEffortCount { get; set; }

        public int HighEffortCount { get; set; }

        public int VeryHighEffortCount { get; set; }
    }
}