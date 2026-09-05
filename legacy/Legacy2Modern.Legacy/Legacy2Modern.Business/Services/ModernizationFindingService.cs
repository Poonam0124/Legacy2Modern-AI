using Legacy2Modern.Business.Models;
using Legacy2Modern.Business.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Legacy2Modern.Business.Services
{
    public class ModernizationFindingService
    {
        private readonly IModernizationFindingRepository _repository;

        public ModernizationFindingService()
        {
            _repository = new ModernizationFindingRepository();
        }

        public List<ModernizationFinding> GetLegacyFindings()
        {
            return _repository.GetAll();
        }

        public ModernizationFindingSummary GetSummary()
        {
            var findings = _repository.GetAll();

            var summary = new ModernizationFindingSummary
            {
                TotalFindings = findings.Count,
                HighOrCriticalRiskCount =
                    findings.Count(x =>
                        x.Risk == ModernizationRisk.High ||
                        x.Risk == ModernizationRisk.Critical),

                HighOrCriticalPriorityCount =
                    findings.Count(x =>
                        x.Priority == ModernizationPriority.High ||
                        x.Priority == ModernizationPriority.Critical),

                IdentifiedCount =
                    findings.Count(x =>
                        x.Status == ModernizationFindingStatus.Identified),

                LowEffortCount =
                    findings.Count(x =>
                        x.EstimatedEffort == ModernizationEffort.Low),

                MediumEffortCount =
                    findings.Count(x =>
                        x.EstimatedEffort == ModernizationEffort.Medium),

                HighEffortCount =
                    findings.Count(x =>
                        x.EstimatedEffort == ModernizationEffort.High),

                VeryHighEffortCount =
                    findings.Count(x =>
                        x.EstimatedEffort == ModernizationEffort.VeryHigh)
            };

            return summary;
        }

        public List<ModernizationFindingExport> GetExportData()
        {
            var findings = _repository.GetAll();

            return findings
                 .Select(x => new ModernizationFindingExport
                 {
                     Id = x.Id,
                     Category = x.Category,
                     Title = x.Title,
                     Description = x.Description,
                     Evidence = x.Evidence,
                     Risk = x.Risk.ToString(),
                     Recommendation = x.Recommendation,
                     Priority = x.Priority.ToString(),
                     AffectedLayer = x.AffectedLayer,
                     ModernizationType = x.ModernizationType,
                     Status = x.Status.ToString(),
                     EstimatedEffort = x.EstimatedEffort.ToString(),
                     RecommendedStrategy = x.RecommendedStrategy.ToString(),
                     ExpectedBenefit = x.ExpectedBenefit
                 })
                 .ToList();
        }
    }
}