
using Legacy2Modern.Business.Models;
using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services.AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Legacy2Modern.Business.Tests
{
    [TestClass]
    public class ModernizationDependencyImpactRequestBuilderTests
    {
        [TestMethod]
        public void Constructor_NullPromptBuilder_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(
                () => new ModernizationDependencyImpactRequestBuilder(null));

            Assert.AreEqual("promptBuilder", exception.ParamName);
        }

        [TestMethod]
        public void Build_NullContext_ThrowsArgumentNullException()
        {
            var builder = CreateBuilder(new TestPromptBuilder());

            var exception = Assert.ThrowsException<ArgumentNullException>(
                () => builder.Build(null));

            Assert.AreEqual("context", exception.ParamName);
        }

        [TestMethod]
        public void Build_EmptyApplicationName_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.ApplicationName = "";

            var builder = CreateBuilder(new TestPromptBuilder());

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => builder.Build(context));

            Assert.AreEqual("Application name is required.", exception.Message);
        }

        [TestMethod]
        public void Build_WhitespaceApplicationName_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.ApplicationName = "   ";

            var builder = CreateBuilder(new TestPromptBuilder());

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => builder.Build(context));

            Assert.AreEqual("Application name is required.", exception.Message);
        }

        [TestMethod]
        public void Build_NullFindings_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.Findings = null;

            var builder = CreateBuilder(new TestPromptBuilder());

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

            var builder = CreateBuilder(new TestPromptBuilder());

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

            var builder = CreateBuilder(new TestPromptBuilder());

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

            var builder = CreateBuilder(new TestPromptBuilder());

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => builder.Build(context));

            Assert.AreEqual(
                "Modernization recommendations are required.",
                exception.Message);
        }

        [TestMethod]
        public void Build_NullPlan_ThrowsInvalidOperationException()
        {
            var context = CreateValidContext();
            context.Plan = null;

            var builder = CreateBuilder(new TestPromptBuilder());

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => builder.Build(context));

            Assert.AreEqual(
                "Modernization plan is required.",
                exception.Message);
        }

        [TestMethod]
        public void Build_ValidContext_CreatesRequestAndBuildsPrompt()
        {
            var context = CreateValidContext();
            var expectedPrompt = new ModernizationPrompt();

            var promptBuilder = new TestPromptBuilder
            {
                PromptToReturn = expectedPrompt
            };

            var builder = CreateBuilder(promptBuilder);

            var request = builder.Build(context);

            Assert.IsNotNull(request);
            Assert.AreSame(context, promptBuilder.ReceivedContext);

            Assert.AreEqual(context.ApplicationName, request.ApplicationName);
            Assert.AreEqual(
                context.ApplicationDescription,
                request.ApplicationDescription);
            Assert.AreEqual(context.TechnologyStack, request.TechnologyStack);
            Assert.AreEqual(context.ModernizationGoal, request.ModernizationGoal);
            Assert.AreSame(expectedPrompt, request.Prompt);
        }

        private static ModernizationDependencyImpactRequestBuilder CreateBuilder(
            IModernizationDependencyImpactPromptBuilder promptBuilder)
        {
            return new ModernizationDependencyImpactRequestBuilder(
                promptBuilder);
        }

        private static ModernizationDependencyImpactContext CreateValidContext()
        {
            return new ModernizationDependencyImpactContext
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
                },
                Plan = new ModernizationPlan()
            };
        }

        private sealed class TestPromptBuilder
            : IModernizationDependencyImpactPromptBuilder
        {
            public ModernizationDependencyImpactContext ReceivedContext
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
                ModernizationDependencyImpactContext context)
            {
                ReceivedContext = context;
                return PromptToReturn;
            }
        }
    }
}
