
using System;
using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services.AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Legacy2Modern.Business.Tests.Services.AI
{
    [TestClass]
    public class ModernizationPlanningResponseParserTests
    {
        private ModernizationPlanningResponseParser _parser;

        [TestInitialize]
        public void TestInitialize()
        {
            _parser = new ModernizationPlanningResponseParser();
        }

        [TestMethod]
        public void Parse_NullResponse_ThrowsInvalidOperationException()
        {
            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => _parser.Parse(null));

            Assert.AreEqual(
                "AI planning response was empty.",
                exception.Message);
        }

        [TestMethod]
        public void Parse_EmptyResponse_ThrowsInvalidOperationException()
        {
            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => _parser.Parse("   "));

            Assert.AreEqual(
                "AI planning response was empty.",
                exception.Message);
        }

        [TestMethod]
        public void Parse_ValidPlanningResponse_ReturnsResponseWithPlan()
        {
            var json = @"{
                ""Plan"": {
                    ""PlanTitle"": ""Modernization Plan"",
                    ""OverallStrategy"": ""Incremental modernization"",
                    ""TargetArchitecture"": ""ASP.NET Core Web API""
                },
                ""ProviderName"": ""Mock""
            }";

            var result = _parser.Parse(json);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Plan);
            Assert.AreEqual("Modernization Plan", result.Plan.PlanTitle);
            Assert.AreEqual(
                "Incremental modernization",
                result.Plan.OverallStrategy);
            Assert.AreEqual(
                "ASP.NET Core Web API",
                result.Plan.TargetArchitecture);
            Assert.AreEqual("Mock", result.ProviderName);
        }

        [TestMethod]
        public void Parse_StandalonePlan_ReturnsWrappedPlanningResponse()
        {
            var json = @"{
                ""PlanTitle"": ""Standalone Plan"",
                ""OverallStrategy"": ""Modernize incrementally"",
                ""TargetArchitecture"": ""ASP.NET Core""
            }";

            var result = _parser.Parse(json);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Plan);
            Assert.AreEqual("Standalone Plan", result.Plan.PlanTitle);
            Assert.AreEqual(
                "Modernize incrementally",
                result.Plan.OverallStrategy);
            Assert.AreEqual(
                "ASP.NET Core",
                result.Plan.TargetArchitecture);
        }

        [TestMethod]
        public void Parse_MarkdownCodeFence_ReturnsParsedResponse()
        {
            var json = @"```json
{
    ""Plan"": {
        ""PlanTitle"": ""Fenced Plan"",
        ""OverallStrategy"": ""Phased approach""
    },
    ""ProviderName"": ""Ollama""
}
```";

            var result = _parser.Parse(json);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Plan);
            Assert.AreEqual("Fenced Plan", result.Plan.PlanTitle);
            Assert.AreEqual(
                "Phased approach",
                result.Plan.OverallStrategy);
            Assert.AreEqual("Ollama", result.ProviderName);
        }

        [TestMethod]
        public void Parse_JsonSurroundedByExplanatoryText_ReturnsParsedResponse()
        {
            var rawResponse =
                "Here is the requested plan:\n" +
                @"{""Plan"":{""PlanTitle"":""Extracted Plan""},""ProviderName"":""Mock""}" +
                "\nLet me know if you need any changes.";

            var result = _parser.Parse(rawResponse);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Plan);
            Assert.AreEqual("Extracted Plan", result.Plan.PlanTitle);
            Assert.AreEqual("Mock", result.ProviderName);
        }

        [TestMethod]
        public void Parse_MalformedJson_ThrowsInvalidOperationException()
        {
            var malformedJson =
                @"{""Plan"": {""PlanTitle"": ""Broken Plan"" ";

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => _parser.Parse(malformedJson));

            StringAssert.Contains(
                exception.Message,
                "AI planning response could not be mapped " +
                "to the expected structure.");

            Assert.IsNotNull(exception.InnerException);
        }
    }
}
