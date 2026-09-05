namespace Legacy2Modern.Business.Models.AI
{
    public class AIProviderConfiguration
    {
        public string ProviderName { get; set; }

        public string ModelName { get; set; }

        public string Endpoint { get; set; }

        public int TimeoutSeconds { get; set; }
    }
}