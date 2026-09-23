using System;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationPlanningResponseValidator
        : IModernizationPlanningResponseValidator
    {
        public ModernizationPlanValidationResult Validate(
            ModernizationPlanningResponse response)
        {
            if (response == null)
            {
                return Invalid(
                    "Planning response cannot be null.");
            }

            if (response.Plan == null)
            {
                return Invalid(
                    "Planning response does not contain a plan.");
            }

            if (string.IsNullOrWhiteSpace(
                response.Plan.PlanTitle))
            {
                return Invalid(
                    "Planning plan title is required.");
            }

            if (string.IsNullOrWhiteSpace(
                response.Plan.OverallStrategy))
            {
                return Invalid(
                    "Planning overall strategy is required.");
            }

            if (string.IsNullOrWhiteSpace(
                response.Plan.TargetArchitecture))
            {
                return Invalid(
                    "Planning target architecture is required.");
            }

            if (response.Plan.Phases == null ||
                response.Plan.Phases.Count == 0)
            {
                return Invalid(
                    "Planning response must contain at least one phase.");
            }

            if (response.Plan.Items == null ||
                response.Plan.Items.Count == 0)
            {
                return Invalid(
                    "Planning response must contain at least one plan item.");
            }

            foreach (var phase in response.Plan.Phases)
            {
                if (phase.PhaseNumber <= 0)
                {
                    return Invalid(
                        "Planning phase number must be greater than zero.");
                }

                if (string.IsNullOrWhiteSpace(
                    phase.PhaseName))
                {
                    return Invalid(
                        "Planning phase name is required.");
                }

                if (string.IsNullOrWhiteSpace(
                    phase.Objective))
                {
                    return Invalid(
                        "Planning phase objective is required.");
                }
            }

            foreach (var item in response.Plan.Items)
            {
                if (string.IsNullOrWhiteSpace(
                    item.PlanItemId))
                {
                    return Invalid(
                        "Planning item ID is required.");
                }

                if (item.PhaseNumber <= 0)
                {
                    return Invalid(
                        "Planning item phase number must be greater than zero.");
                }

                if (string.IsNullOrWhiteSpace(
                    item.Title))
                {
                    return Invalid(
                        "Planning item title is required.");
                }

                if (string.IsNullOrWhiteSpace(
                    item.RecommendedAction))
                {
                    return Invalid(
                        "Planning item recommended action is required.");
                }
            }

            return new ModernizationPlanValidationResult
            {
                IsValid = true
            };
        }

        private ModernizationPlanValidationResult Invalid(
            string message)
        {
            return new ModernizationPlanValidationResult
            {
                IsValid = false,
                ErrorMessage = message
            };
        }
    }
}