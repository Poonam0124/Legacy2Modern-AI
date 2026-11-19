
using Legacy2Modern.Business.Models;
using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services.AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Legacy2Modern.Business.Tests
{
    [TestClass]
    public class ModernizationPlanningRequestBuilderTests
    {
        [TestMethod]
        public void Build_NullContext_ThrowsArgumentNullException()
        {
            var builder = new ModernizationPlanningRequestBuilder(
                new TestPromptBuilder());

            var exception = Assert.ThrowsException<ArgumentNullException>(
                () => builder.Build(null));

            Assert.AreEqual("context", exception.ParamName);
        }

        [TestMethod]
        public void Build_NullFindings_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.Findings = null;

            var builder = new ModernizationPlanningRequestBuilder(
                new TestPromptBuilder());

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => builder.Build(context));

            Assert.AreEqual(
                "Modernization findings are required.",
                exception.Message);
        }

        [TestMethod]
        public void Build_EmptyFindings_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.Findings.Clear();

            var builder = new ModernizationPlanningRequestBuilder(
                new TestPromptBuilder());

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => builder.Build(context));

            Assert.AreEqual(
                "Modernization findings are required.",
                exception.Message);
        }

        [TestMethod]
        public void Build_NullRecommendations_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.Recommendations = null;

            var builder = new ModernizationPlanningRequestBuilder(
                new TestPromptBuilder());

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => builder.Build(context));

            Assert.AreEqual(
                "Modernization recommendations are required.",
                exception.Message);
        }

        [TestMethod]
        public void Build_EmptyRecommendations_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.Recommendations.Clear();

            var builder = new ModernizationPlanningRequestBuilder(
                new TestPromptBuilder());

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => builder.Build(context));

            Assert.AreEqual(
                "Modernization recommendations are required.",
                exception.Message);
        }

        [TestMethod]
        public void Build_ValidContext_CopiesFieldsAndPrompt()
        {
            var context = CreateValidContext();
            var expectedPrompt = new ModernizationPrompt();

            var promptBuilder = new TestPromptBuilder
            {
                PromptToReturn = expectedPrompt
            };

            var builder = new ModernizationPlanningRequestBuilder(
                promptBuilder);

            var request = builder.Build(context);

            Assert.IsNotNull(request);
            Assert.AreSame(context, promptBuilder.ReceivedContext);

            Assert.AreEqual(context.ApplicationName, request.ApplicationName);
            Assert.AreEqual(
                context.ApplicationDescription,
                request.ApplicationDescription);
            Assert.AreEqual(context.TechnologyStack, request.TechnologyStack);
            Assert.AreEqual(context.ModernizationGoal, request.ModernizationGoal);

            Assert.AreSame(context.Findings, request.Findings);
            Assert.AreSame(context.Recommendations, request.Recommendations);
            Assert.AreSame(expectedPrompt, request.Prompt);
        }

        private static ModernizationPlanningContext CreateValidContext()
        {
            return new ModernizationPlanningContext
            {
                ApplicationName = "Test Application",
                ApplicationDescription = "Test description",
                TechnologyStack = ".NET Framework 4.8",
                ModernizationGoal = "Modernize to ASP.NET Core",
                Findings = new List<ModernizationFindingExport>
                {
                    new ModernizationFindingExport()
                },
                Recommendations = new List<ModernizationRecommendation>
                {
                    new ModernizationRecommendation()
                }
            };
        }

        private sealed class TestPromptBuilder
            : IModernizationPlanningPromptBuilder
        {
            public ModernizationPlanningContext ReceivedContext
            {
                get;
                private set;
            }

            public ModernizationPrompt PromptToReturn
            {
                get;
                set;
            }

            public ModernizationPrompt Build(
                ModernizationPlanningContext context)
            {
                ReceivedContext = context;
                return PromptToReturn;
            }
        }
    }
}
