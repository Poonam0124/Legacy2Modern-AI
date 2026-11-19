
using Legacy2Modern.Business.Models;
using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services.AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Legacy2Modern.Business.Tests.Services.AI
{
    [TestClass]
    public class ModernizationPlanningPromptBuilderTests
    {
        private ModernizationPlanningPromptBuilder _builder;

        [TestInitialize]
        public void TestInitialize()
        {
            _builder = new ModernizationPlanningPromptBuilder();
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
                "software modernization planning architect");

            StringAssert.Contains(
                result.UserInstruction,
                "Create a modernization implementation plan");

            StringAssert.Contains(
                result.UserInstruction,
                "Return the modernization plan as JSON only.");
        }

        [TestMethod]
        public void Build_ValidContext_IncludesApplicationDetails()
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
        }

        [TestMethod]
        public void Build_ValidContext_IncludesFindingAndRecommendationDetails()
        {
            var context = CreateValidContext();

            var result = _builder.Build(context);
            var prompt = result.UserInstruction;

            // Finding details
            StringAssert.Contains(prompt, "FINDING-001");
            StringAssert.Contains(prompt, "Legacy data access");
            StringAssert.Contains(
                prompt,
                "Data access uses tightly coupled legacy code.");

            // Recommendation details
            StringAssert.Contains(
                prompt,
                "Separate data access into a dedicated layer.");
            StringAssert.Contains(
                prompt,
                "Improves maintainability.");

            // Both finding and recommendation risk/priority fields
            StringAssert.Contains(prompt, "Priority: High");
            StringAssert.Contains(prompt, "Risk: High");
            StringAssert.Contains(prompt, "Risk: Medium");
            StringAssert.Contains(prompt, "Complexity: Moderate");
        }


        private ModernizationPlanningContext CreateValidContext()
        {
            return new ModernizationPlanningContext
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
                            "Data access uses tightly coupled legacy code.",
                        Risk = "High",
                        Priority = "High"
                    }
                },

                Recommendations =
                    new List<ModernizationRecommendation>
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
                    }
            };
        }
    }
}
