
using Legacy2Modern.Business.Models;
using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services.AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Legacy2Modern.Business.Tests.Services.AI
{
    [TestClass]
    public class ModernizationDependencyImpactPromptBuilderTests
    {
        private ModernizationDependencyImpactPromptBuilder _builder;

        [TestInitialize]
        public void TestInitialize()
        {
            _builder =
                new ModernizationDependencyImpactPromptBuilder();
        }

        [TestMethod]
        public void Build_NullContext_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => _builder.Build(null));
        }

        [TestMethod]
        public void Build_NullFindings_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.Findings = null;

            var exception =
                Assert.ThrowsException<InvalidOperationException>(
                    () => _builder.Build(context));

            Assert.AreEqual(
                "Modernization findings are required.",
                exception.Message);
        }

        [TestMethod]
        public void Build_EmptyFindings_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.Findings =
                new List<ModernizationFindingExport>();

            var exception =
                Assert.ThrowsException<InvalidOperationException>(
                    () => _builder.Build(context));

            Assert.AreEqual(
                "Modernization findings are required.",
                exception.Message);
        }

        [TestMethod]
        public void Build_NullRecommendations_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.Recommendations = null;

            var exception =
                Assert.ThrowsException<InvalidOperationException>(
                    () => _builder.Build(context));

            Assert.AreEqual(
                "Modernization recommendations are required.",
                exception.Message);
        }

        [TestMethod]
        public void Build_EmptyRecommendations_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.Recommendations =
                new List<ModernizationRecommendation>();

            var exception =
                Assert.ThrowsException<InvalidOperationException>(
                    () => _builder.Build(context));

            Assert.AreEqual(
                "Modernization recommendations are required.",
                exception.Message);
        }

        [TestMethod]
        public void Build_NullPlan_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.Plan = null;

            var exception =
                Assert.ThrowsException<InvalidOperationException>(
                    () => _builder.Build(context));

            Assert.AreEqual(
                "Modernization plan is required.",
                exception.Message);
        }

        [TestMethod]
        public void Build_NullPlanItems_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.Plan.Items = null;

            var exception =
                Assert.ThrowsException<InvalidOperationException>(
                    () => _builder.Build(context));

            Assert.AreEqual(
                "Modernization plan items are required.",
                exception.Message);
        }

        [TestMethod]
        public void Build_EmptyPlanItems_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.Plan.Items =
                new List<ModernizationPlanItem>();

            var exception =
                Assert.ThrowsException<InvalidOperationException>(
                    () => _builder.Build(context));

            Assert.AreEqual(
                "Modernization plan items are required.",
                exception.Message);
        }

        [TestMethod]
        public void Build_ValidContext_ReturnsPromptWithInstructions()
        {
            var context = CreateValidContext();

            var result = _builder.Build(context);

            Assert.IsNotNull(result);
            Assert.IsFalse(
                string.IsNullOrWhiteSpace(result.SystemInstruction));
            Assert.IsFalse(
                string.IsNullOrWhiteSpace(result.UserInstruction));

            StringAssert.Contains(
                result.SystemInstruction,
                "dependency and impact analysis architect");

            StringAssert.Contains(
                result.UserInstruction,
                "DEPENDENCY REQUIREMENTS");

            StringAssert.Contains(
                result.UserInstruction,
                "IMPACT REQUIREMENTS");

            StringAssert.Contains(
                result.UserInstruction,
                "Return one JSON object matching the required schema.");
        }

        [TestMethod]
        public void Build_ValidContext_IncludesApplicationAndFindingDetails()
        {
            var context = CreateValidContext();

            var result = _builder.Build(context);
            var prompt = result.UserInstruction;

            StringAssert.Contains(prompt, "Portfolio Legacy App");
            StringAssert.Contains(
                prompt,
                "Legacy application for modernization testing");
            StringAssert.Contains(prompt, "ASP.NET Web Forms");
            StringAssert.Contains(
                prompt,
                "Incremental modernization");

            StringAssert.Contains(prompt, "FINDING-001");
            StringAssert.Contains(prompt, "Legacy data access");
            StringAssert.Contains(
                prompt,
                "Tightly coupled legacy data access.");
            StringAssert.Contains(
                prompt,
                "Affected Layer: Data");
        }

        [TestMethod]
        public void Build_ValidContext_IncludesPlanAndDependencyRules()
        {
            var context = CreateValidContext();

            var result = _builder.Build(context);
            var prompt = result.UserInstruction;

            StringAssert.Contains(
                prompt,
                "Separate data access into a dedicated layer.");

            StringAssert.Contains(
                prompt,
                "PLAN ITEMS");

            StringAssert.Contains(
                prompt,
                "Plan Item ID: PLAN-001");

            StringAssert.Contains(
                prompt,
                "Finding ID: FINDING-001");

            StringAssert.Contains(
                prompt,
                "SourceId must identify the prerequisite plan item.");

            StringAssert.Contains(
                prompt,
                "Do not create circular dependencies.");

            StringAssert.Contains(
                prompt,
                "If no meaningful dependency is supported, return " +
                "an empty Dependencies array.");

            StringAssert.Contains(
                prompt,
                "FindingId must exactly match an ID from the " +
                "supplied Modernization Findings.");
        }

        private ModernizationDependencyImpactContext CreateValidContext()
        {
            return new ModernizationDependencyImpactContext
            {
                ApplicationName = "Portfolio Legacy App",
                ApplicationDescription =
                    "Legacy application for modernization testing",
                TechnologyStack = "ASP.NET Web Forms",
                ModernizationGoal = "Incremental modernization",

                Findings = new List<ModernizationFindingExport>
                {
                    new ModernizationFindingExport
                    {
                        Id = "FINDING-001",
                        Title = "Legacy data access",
                        Description =
                            "Tightly coupled legacy data access.",
                        Evidence = "Database calls are embedded in UI code.",
                        Category = "Architecture",
                        Risk = "High",
                        Recommendation =
                            "Separate data access from the UI.",
                        Priority = "High",
                        AffectedLayer = "Data",
                        ModernizationType = "Refactoring",
                        Status = "Identified",
                        EstimatedEffort = "Medium"
                    }
                },

                Recommendations = new List<ModernizationRecommendation>
                {
                    new ModernizationRecommendation
                    {
                        FindingId = "FINDING-001",
                        RecommendedAction =
                            "Separate data access into a dedicated layer.",
                        Reasoning = "Improves maintainability.",
                        Risk = "Medium",
                        Complexity = "Moderate"
                    }
                },

                Plan = new ModernizationPlan
                {
                    PlanTitle = "Incremental Modernization Plan",
                    OverallStrategy = "Modernize in small phases",
                    TargetArchitecture = "ASP.NET Core Web API",

                    Phases = new List<ModernizationPlanPhase>
                    {
                        new ModernizationPlanPhase
                        {
                            PhaseNumber = 1,
                            PhaseName = "Preparation",
                            Objective = "Prepare the data layer",
                            Rationale = "Reduce migration risk"
                        }
                    },

                    Items = new List<ModernizationPlanItem>
                    {
                        new ModernizationPlanItem
                        {
                            PlanItemId = "PLAN-001",
                            PhaseNumber = 1,
                            FindingId = "FINDING-001",
                            Title = "Separate data access",
                            RecommendedAction =
                                "Create a dedicated data access layer.",
                            Dependencies = new List<string>(),
                            AffectedComponents =
                                new List<string> { "Data", "Web" },
                            ImplementationSteps =
                                new List<string>
                                {
                                    "Identify database calls",
                                    "Extract data access logic"
                                }
                        }
                    }
                }
            };
        }
    }
}
