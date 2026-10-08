using System;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationDependencyImpactAnalysisService
        : IModernizationDependencyImpactAnalysisService
    {
        private readonly
            IModernizationDependencyImpactRequestBuilder
            _requestBuilder;

        private readonly
            IAIDependencyImpactProvider
            _provider;

        public ModernizationDependencyImpactAnalysisService(
            IModernizationDependencyImpactRequestBuilder requestBuilder,
            IAIDependencyImpactProvider provider)
        {
            _requestBuilder = requestBuilder
                ?? throw new ArgumentNullException(
                    "requestBuilder");

            _provider = provider
                ?? throw new ArgumentNullException(
                    "provider");
        }

        public ModernizationDependencyImpactAnalysis Analyze(
            ModernizationDependencyImpactContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(
                    "context");
            }

            var request =
                _requestBuilder.Build(context);

            var analysis =
                _provider.Analyze(request);

            if (analysis == null)
            {
                throw new InvalidOperationException(
                    "Dependency and impact analysis response is null.");
            }

            return analysis;
        }
    }
}