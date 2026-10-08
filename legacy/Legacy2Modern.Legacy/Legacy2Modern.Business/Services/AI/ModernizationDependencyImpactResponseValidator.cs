using System;
using System.Collections.Generic;
using System.Linq;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationDependencyImpactResponseValidator
        : IModernizationDependencyImpactResponseValidator
    {
        public ModernizationDependencyImpactValidationResult Validate(
            ModernizationDependencyImpactAnalysis analysis)
        {
            if (analysis == null)
            {
                return Invalid(
                    "Dependency and impact analysis is required.");
            }

            if (string.IsNullOrWhiteSpace(
                analysis.AnalysisTitle))
            {
                return Invalid(
                    "Analysis title is required.");
            }

            if (string.IsNullOrWhiteSpace(
                analysis.OverallAssessment))
            {
                return Invalid(
                    "Overall assessment is required.");
            }

            if (analysis.Dependencies == null)
            {
                return Invalid(
                    "Dependencies collection is required.");
            }

            if (analysis.Impacts == null)
            {
                return Invalid(
                    "Impacts collection is required.");
            }

            foreach (var dependency in
                analysis.Dependencies)
            {
                if (dependency == null)
                {
                    return Invalid(
                        "Dependency cannot be null.");
                }

                if (string.IsNullOrWhiteSpace(
                    dependency.DependencyId))
                {
                    return Invalid(
                        "Dependency ID is required.");
                }

                if (string.IsNullOrWhiteSpace(
                    dependency.SourceId))
                {
                    return Invalid(
                        "Dependency source ID is required.");
                }

                if (string.IsNullOrWhiteSpace(
                    dependency.TargetId))
                {
                    return Invalid(
                        "Dependency target ID is required.");
                }

                if (string.IsNullOrWhiteSpace(
                    dependency.DependencyType))
                {
                    return Invalid(
                        "Dependency type is required.");
                }

                if (string.IsNullOrWhiteSpace(
                    dependency.Description))
                {
                    return Invalid(
                        "Dependency description is required.");
                }

                if (string.IsNullOrWhiteSpace(
                    dependency.Impact))
                {
                    return Invalid(
                        "Dependency impact is required.");
                }

                if (dependency.AffectedComponents == null)
                {
                    return Invalid(
                        "Dependency affected components are required.");
                }

                if (dependency.Reasons == null)
                {
                    return Invalid(
                        "Dependency reasons are required.");
                }
            }

            var dependencyIds =
                new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase);

            foreach (var dependency in
                analysis.Dependencies)
            {
                if (!dependencyIds.Add(
                    dependency.DependencyId))
                {
                    return Invalid(
                        "Duplicate dependency ID: " +
                        dependency.DependencyId);
                }
            }

            foreach (var impact in analysis.Impacts)
            {
                if (impact == null)
                {
                    return Invalid(
                        "Impact cannot be null.");
                }

                if (string.IsNullOrWhiteSpace(
                    impact.ImpactId))
                {
                    return Invalid(
                        "Impact ID is required.");
                }

                if (string.IsNullOrWhiteSpace(
                    impact.FindingId))
                {
                    return Invalid(
                        "Impact finding ID is required.");
                }

                if (string.IsNullOrWhiteSpace(
                    impact.ImpactArea))
                {
                    return Invalid(
                        "Impact area is required.");
                }

                if (string.IsNullOrWhiteSpace(
                    impact.ImpactLevel))
                {
                    return Invalid(
                        "Impact level is required.");
                }

                if (string.IsNullOrWhiteSpace(
                    impact.Description))
                {
                    return Invalid(
                        "Impact description is required.");
                }

                if (impact.AffectedComponents == null)
                {
                    return Invalid(
                        "Impact affected components are required.");
                }

                if (impact.Risks == null)
                {
                    return Invalid(
                        "Impact risks are required.");
                }

                if (impact.Mitigations == null)
                {
                    return Invalid(
                        "Impact mitigations are required.");
                }
            }

            var impactIds =
                new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase);

            foreach (var impact in
                analysis.Impacts)
            {
                if (!impactIds.Add(
                    impact.ImpactId))
                {
                    return Invalid(
                        "Duplicate impact ID: " +
                        impact.ImpactId);
                }
            }

            return new ModernizationDependencyImpactValidationResult
            {
                IsValid = true,
                ErrorMessage = null
            };
        }

        private ModernizationDependencyImpactValidationResult Invalid(
            string message)
        {
            return new ModernizationDependencyImpactValidationResult
            {
                IsValid = false,
                ErrorMessage = message
            };
        }
    }
}