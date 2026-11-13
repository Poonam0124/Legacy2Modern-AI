
using Legacy2Modern.Business.Models;
using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services.AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Legacy2Modern.Business.Tests
{
    [TestClass]
    public class ModernizationDependencyImpactResponseValidatorTests
    {
        private ModernizationDependencyImpactResponseValidator _validator;

        [TestInitialize]
        public void SetUp()
        {
            _validator =
                new ModernizationDependencyImpactResponseValidator();
        }

        [TestMethod]
        public void Validate_WhenAnalysisIsValid_ReturnsValidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>(),
                Impacts = new List<ModernizationImpact>()
            };

            var result = _validator.Validate(analysis);

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.ErrorMessage);
        }

        [TestMethod]
        public void Validate_WhenAnalysisTitleIsMissing_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = " ",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>(),
                Impacts = new List<ModernizationImpact>()
            };

            var result = _validator.Validate(analysis);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Analysis title is required.",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenDependencyIdsAreDuplicated_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>
        {
            new ModernizationDependency
            {
                DependencyId = "DEP-001",
                SourceId = "PLAN-001",
                TargetId = "PLAN-002",
                DependencyType = "Prerequisite",
                Description = "First dependency.",
                Impact = "Affects implementation order.",
                AffectedComponents = new List<string>(),
                Reasons = new List<string>()
            },
            new ModernizationDependency
            {
                DependencyId = "dep-001",
                SourceId = "PLAN-002",
                TargetId = "PLAN-003",
                DependencyType = "Prerequisite",
                Description = "Second dependency.",
                Impact = "Affects implementation order.",
                AffectedComponents = new List<string>(),
                Reasons = new List<string>()
            }
        },
                Impacts = new List<ModernizationImpact>()
            };

            var result = _validator.Validate(analysis);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Duplicate dependency ID: dep-001",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenImpactIdsAreDuplicated_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>(),
                Impacts = new List<ModernizationImpact>
        {
            new ModernizationImpact
            {
                ImpactId = "IMP-001",
                FindingId = "F001",
                ImpactArea = "Architecture",
                ImpactLevel = "High",
                Description = "First impact.",
                AffectedComponents = new List<string>(),
                Risks = new List<string>(),
                Mitigations = new List<string>()
            },
            new ModernizationImpact
            {
                ImpactId = "imp-001",
                FindingId = "F002",
                ImpactArea = "Configuration",
                ImpactLevel = "Medium",
                Description = "Second impact.",
                AffectedComponents = new List<string>(),
                Risks = new List<string>(),
                Mitigations = new List<string>()
            }
        }
            };

            var result = _validator.Validate(analysis);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Duplicate impact ID: imp-001",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenDependencyReferencesUnknownSourcePlanItem_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>
        {
            new ModernizationDependency
            {
                DependencyId = "DEP-001",
                SourceId = "UNKNOWN-PLAN",
                TargetId = "PLAN-001",
                DependencyType = "Prerequisite",
                Description = "Source plan item must exist.",
                Impact = "Affects implementation order.",
                AffectedComponents = new List<string>(),
                Reasons = new List<string>()
            }
        },
                Impacts = new List<ModernizationImpact>()
            };

            var context = new ModernizationDependencyImpactContext
            {
                Plan = new ModernizationPlan
                {
                    Items = new List<ModernizationPlanItem>
            {
                new ModernizationPlanItem
                {
                    PlanItemId = "PLAN-001"
                }
            }
                },
                Findings = new List<ModernizationFindingExport>()
            };

            var result = _validator.Validate(analysis, context);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Dependency 'DEP-001' references an unknown source plan item: UNKNOWN-PLAN",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenDependencyReferencesUnknownTargetPlanItem_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>
        {
            new ModernizationDependency
            {
                DependencyId = "DEP-002",
                SourceId = "PLAN-001",
                TargetId = "UNKNOWN-PLAN",
                DependencyType = "Prerequisite",
                Description = "Target plan item must exist.",
                Impact = "Affects implementation order.",
                AffectedComponents = new List<string>(),
                Reasons = new List<string>()
            }
        },
                Impacts = new List<ModernizationImpact>()
            };

            var context = new ModernizationDependencyImpactContext
            {
                Plan = new ModernizationPlan
                {
                    Items = new List<ModernizationPlanItem>
            {
                new ModernizationPlanItem
                {
                    PlanItemId = "PLAN-001"
                }
            }
                },
                Findings = new List<ModernizationFindingExport>()
            };

            var result = _validator.Validate(analysis, context);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Dependency 'DEP-002' references an unknown target plan item: UNKNOWN-PLAN",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenDependencyReferencesItself_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>
        {
            new ModernizationDependency
            {
                DependencyId = "DEP-003",
                SourceId = "PLAN-001",
                TargetId = "PLAN-001",
                DependencyType = "Prerequisite",
                Description = "A plan item cannot depend on itself.",
                Impact = "Creates an invalid dependency.",
                AffectedComponents = new List<string>(),
                Reasons = new List<string>()
            }
        },
                Impacts = new List<ModernizationImpact>()
            };

            var context = new ModernizationDependencyImpactContext
            {
                Plan = new ModernizationPlan
                {
                    Items = new List<ModernizationPlanItem>
            {
                new ModernizationPlanItem
                {
                    PlanItemId = "PLAN-001"
                }
            }
                },
                Findings = new List<ModernizationFindingExport>()
            };

            var result = _validator.Validate(analysis, context);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Dependency 'DEP-003' cannot depend on itself.",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenImpactReferencesUnknownFinding_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>(),
                Impacts = new List<ModernizationImpact>
        {
            new ModernizationImpact
            {
                ImpactId = "IMP-001",
                FindingId = "UNKNOWN-FINDING",
                ImpactArea = "Architecture",
                ImpactLevel = "High",
                Description = "This impact references a missing finding.",
                AffectedComponents = new List<string>(),
                Risks = new List<string>(),
                Mitigations = new List<string>()
            }
        }
            };

            var context = new ModernizationDependencyImpactContext
            {
                Plan = new ModernizationPlan
                {
                    Items = new List<ModernizationPlanItem>
            {
                new ModernizationPlanItem
                {
                    PlanItemId = "PLAN-001"
                }
            }
                },
                Findings = new List<ModernizationFindingExport>
        {
            new ModernizationFindingExport
            {
                Id = "F001"
            }
        }
            };

            var result = _validator.Validate(analysis, context);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Impact 'IMP-001' references an unknown finding: UNKNOWN-FINDING",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenContextIsNull_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>(),
                Impacts = new List<ModernizationImpact>()
            };

            var result = _validator.Validate(
                analysis,
                (ModernizationDependencyImpactContext)null);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Dependency and impact analysis context is required.",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenModernizationPlanIsNull_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>(),
                Impacts = new List<ModernizationImpact>()
            };

            var context = new ModernizationDependencyImpactContext
            {
                Plan = null,
                Findings = new List<ModernizationFindingExport>()
            };

            var result = _validator.Validate(analysis, context);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Modernization plan items are required for validation.",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenPlanItemsAreNull_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>(),
                Impacts = new List<ModernizationImpact>()
            };

            var context = new ModernizationDependencyImpactContext
            {
                Plan = new ModernizationPlan
                {
                    Items = null
                },
                Findings = new List<ModernizationFindingExport>()
            };

            var result = _validator.Validate(analysis, context);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Modernization plan items are required for validation.",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenFindingsAreNull_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>(),
                Impacts = new List<ModernizationImpact>()
            };

            var context = new ModernizationDependencyImpactContext
            {
                Plan = new ModernizationPlan
                {
                    Items = new List<ModernizationPlanItem>()
                },
                Findings = null
            };

            var result = _validator.Validate(analysis, context);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Modernization findings are required for validation.",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenAnalysisIsNull_ReturnsInvalidResult()
        {
            var result = _validator.Validate(
                (ModernizationDependencyImpactAnalysis)null);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Dependency and impact analysis is required.",
                result.ErrorMessage);
        }

        [TestMethod]
        public void Validate_WhenOverallAssessmentIsMissing_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = " ",
                Dependencies = new List<ModernizationDependency>(),
                Impacts = new List<ModernizationImpact>()
            };

            var result = _validator.Validate(analysis);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Overall assessment is required.",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenDependenciesAreNull_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = null,
                Impacts = new List<ModernizationImpact>()
            };

            var result = _validator.Validate(analysis);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Dependencies collection is required.",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenImpactsAreNull_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>(),
                Impacts = null
            };

            var result = _validator.Validate(analysis);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Impacts collection is required.",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenDependencyEntryIsNull_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>
        {
            null
        },
                Impacts = new List<ModernizationImpact>()
            };

            var result = _validator.Validate(analysis);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Dependency cannot be null.",
                result.ErrorMessage);
        }


        [TestMethod]
        public void Validate_WhenImpactEntryIsNull_ReturnsInvalidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>(),
                Impacts = new List<ModernizationImpact>
        {
            null
        }
            };

            var result = _validator.Validate(analysis);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(
                "Impact cannot be null.",
                result.ErrorMessage);
        }



        [TestMethod]
        public void Validate_WhenPlanItemReferenceDiffersOnlyByCase_ReturnsValidResult()
        {
            var analysis = new ModernizationDependencyImpactAnalysis
            {
                AnalysisTitle = "Dependency and Impact Analysis",
                OverallAssessment = "Modernization plan reviewed.",
                Dependencies = new List<ModernizationDependency>
        {
            new ModernizationDependency
            {
                DependencyId = "DEP-001",
                SourceId = "plan-001",
                TargetId = "PLAN-002",
                DependencyType = "Prerequisite",
                Description = "A case-insensitive reference test.",
                Impact = "Affects implementation order.",
                AffectedComponents = new List<string>(),
                Reasons = new List<string>()
            }
        },
                Impacts = new List<ModernizationImpact>()
            };

            var context = new ModernizationDependencyImpactContext
            {
                Plan = new ModernizationPlan
                {
                    Items = new List<ModernizationPlanItem>
            {
                new ModernizationPlanItem
                {
                    PlanItemId = "PLAN-001"
                },
                new ModernizationPlanItem
                {
                    PlanItemId = "PLAN-002"
                }
            }
                },
                Findings = new List<ModernizationFindingExport>()
            };

            var result = _validator.Validate(analysis, context);

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.ErrorMessage);
        }

    }
}
