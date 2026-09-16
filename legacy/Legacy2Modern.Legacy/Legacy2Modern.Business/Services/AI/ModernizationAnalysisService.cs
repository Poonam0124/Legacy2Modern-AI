using System;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationAnalysisService
        : IModernizationAnalysisService
    {
        private readonly ModernizationFindingService _findingService;
        private readonly IModernizationPromptBuilder _promptBuilder;
        private readonly IModernizationAIRequestBuilder _requestBuilder;
        private readonly IModernizationAIService _aiService;

        public ModernizationAnalysisService(
            ModernizationFindingService findingService,
            IModernizationPromptBuilder promptBuilder,
            IModernizationAIRequestBuilder requestBuilder,
            IModernizationAIService aiService)
        {
            _findingService = findingService;
            _promptBuilder = promptBuilder;
            _requestBuilder = requestBuilder;
            _aiService = aiService;
        }

        public ModernizationAnalysisResponse Analyze()
        {
            var findings =
                _findingService.GetExportData();

            if (findings == null || findings.Count == 0)
                throw new InvalidOperationException(
                    "No modernization findings are available.");

            var context =
                new ModernizationAnalysisContext
                {
                    ApplicationName =
                        "Legacy2Modern-AI",

                    ApplicationDescription =
                        "A legacy ASP.NET Web Forms application being incrementally modernized.",

                    TechnologyStack =
                        "ASP.NET Web Forms, .NET Framework 4.8, C#, Entity Framework 6, SQL Server",

                    ModernizationGoal =
                        "Identify practical modernization opportunities while minimizing business disruption.",

                    Findings = findings
                };

            var request =
                _requestBuilder.Build(context);

            return _aiService.Analyze(request);
        }
    }
}