namespace Legacy2Modern.Business.Models.AI
{
    public class ModernizationDependencyImpactRequest
    {
        public string ApplicationName { get; set; }

        public string ApplicationDescription { get; set; }

        public string TechnologyStack { get; set; }

        public string ModernizationGoal { get; set; }

        public ModernizationPrompt Prompt { get; set; }
    }
}