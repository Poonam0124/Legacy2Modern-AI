using System;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationPlanningService
        : IModernizationPlanningService
    {
        private readonly IModernizationPlanningRequestBuilder _requestBuilder;
        private readonly IModernizationAIService _aiService;

        public ModernizationPlanningService(
            IModernizationPlanningRequestBuilder requestBuilder,
            IModernizationAIService aiService)
        {
            _requestBuilder = requestBuilder;
            _aiService = aiService;
        }

        public ModernizationPlanningResponse CreatePlan(
            ModernizationPlanningContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var request = _requestBuilder.Build(context);

            var analysisResponse =
                _aiService.Analyze(
                    new ModernizationAnalysisRequest
                    {
                        ApplicationName =
                            request.ApplicationName,

                        TechnologyStack =
                            request.TechnologyStack,

                        Findings =
                            request.Findings,

                        Prompt =
                            request.Prompt
                    });

            return ConvertToPlanningResponse(
                analysisResponse);
        }

        private ModernizationPlanningResponse ConvertToPlanningResponse(
            ModernizationAnalysisResponse response)
        {
            if (response == null)
                throw new InvalidOperationException(
                    "AI planning response was empty.");

            return new ModernizationPlanningResponse
            {
                Plan = new ModernizationPlan
                {
                    PlanTitle =
                        "Legacy2Modern-AI Modernization Plan",

                    OverallStrategy =
                        response.OverallAssessment,

                    TargetArchitecture =
                        response.TargetArchitecture,

                    Phases =
                        new System.Collections.Generic.List<ModernizationPlanPhase>(),

                    Items =
                        new System.Collections.Generic.List<ModernizationPlanItem>()
                }
            };
        }
    }
}