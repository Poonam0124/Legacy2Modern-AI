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

        private readonly
            IModernizationDependencyImpactResponseValidator
            _validator;

        public ModernizationDependencyImpactAnalysisService(
            IModernizationDependencyImpactRequestBuilder requestBuilder,
            IAIDependencyImpactProvider provider,
            IModernizationDependencyImpactResponseValidator validator)
        {
            _requestBuilder = requestBuilder
                ?? throw new ArgumentNullException("requestBuilder");

            _provider = provider
                ?? throw new ArgumentNullException("provider");

            _validator = validator
                ?? throw new ArgumentNullException("validator");
        }

        public ModernizationDependencyImpactAnalysis Analyze(
            ModernizationDependencyImpactContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var request = _requestBuilder.Build(context);
            var analysis = _provider.Analyze(request);

            if (analysis == null)
            {
                throw new InvalidOperationException(
                    "Dependency and impact analysis response is null.");
            }

            var validation = _validator.Validate(
                analysis,
                context);

            if (validation == null || !validation.IsValid)
            {
                var message = validation == null
                    ? "Validation returned no result."
                    : validation.ErrorMessage;

                throw new InvalidOperationException(
                    "Dependency and impact analysis validation failed: " +
                    message);
            }

            return analysis;
        }
    }
}