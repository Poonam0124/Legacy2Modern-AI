using System;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationPlanningService
        : IModernizationPlanningService
    {
        private readonly IModernizationPlanningRequestBuilder _requestBuilder;
        private readonly IAIPlanningProvider _planningProvider;

        public ModernizationPlanningService(
            IModernizationPlanningRequestBuilder requestBuilder,
            IAIPlanningProvider planningProvider)
        {
            _requestBuilder = requestBuilder;
            _planningProvider = planningProvider;
        }

        public ModernizationPlanningResponse CreatePlan(
            ModernizationPlanningContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var request =
                _requestBuilder.Build(context);

            var response =
                _planningProvider.CreatePlan(request);

            if (response == null)
            {
                throw new InvalidOperationException(
                    "AI planning provider returned an empty response.");
            }

            return response;
        }
    }
}