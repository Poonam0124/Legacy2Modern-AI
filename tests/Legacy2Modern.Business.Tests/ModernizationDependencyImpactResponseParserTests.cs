
using System;
using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services.AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Legacy2Modern.Business.Tests.Services.AI
{
    [TestClass]
    public class ModernizationDependencyImpactResponseParserTests
    {
        private ModernizationDependencyImpactResponseParser _parser;

        [TestInitialize]
        public void TestInitialize()
        {
            _parser =
                new ModernizationDependencyImpactResponseParser();
        }

        [TestMethod]
        public void Parse_NullResponse_ThrowsInvalidOperationException()
        {
            var exception =
                Assert.ThrowsException<InvalidOperationException>(
                    () => _parser.Parse(null));

            Assert.AreEqual(
                "AI dependency and impact response is empty.",
                exception.Message);
        }

        [TestMethod]
        public void Parse_EmptyResponse_ThrowsInvalidOperationException()
        {
            var exception =
                Assert.ThrowsException<InvalidOperationException>(
                    () => _parser.Parse("   "));

            Assert.AreEqual(
                "AI dependency and impact response is empty.",
                exception.Message);
        }

        [TestMethod]
        public void Parse_ValidJson_ReturnsAnalysis()
        {
            var json = @"{
                ""AnalysisTitle"": ""Dependency Analysis"",
                ""OverallAssessment"": ""Moderate modernization risk"",
                ""Dependencies"": [],
                ""Impacts"": []
            }";

            var result = _parser.Parse(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(
                "Dependency Analysis",
                result.AnalysisTitle);
            Assert.AreEqual(
                "Moderate modernization risk",
                result.OverallAssessment);
            Assert.IsNotNull(result.Dependencies);
            Assert.AreEqual(0, result.Dependencies.Count);
            Assert.IsNotNull(result.Impacts);
            Assert.AreEqual(0, result.Impacts.Count);
        }

        [TestMethod]
        public void Parse_MarkdownCodeFence_ReturnsAnalysis()
        {
            var rawResponse = @"```json
{
    ""AnalysisTitle"": ""Fenced Analysis"",
    ""OverallAssessment"": ""Low risk"",
    ""Dependencies"": [],
    ""Impacts"": []
}
```";

            var result = _parser.Parse(rawResponse);

            Assert.IsNotNull(result);
            Assert.AreEqual(
                "Fenced Analysis",
                result.AnalysisTitle);
            Assert.AreEqual(
                "Low risk",
                result.OverallAssessment);
        }

        [TestMethod]
        public void Parse_JsonSurroundedByExplanatoryText_ReturnsAnalysis()
        {
            var rawResponse =
                "Here is the dependency analysis:\n" +
                @"{""AnalysisTitle"":""Extracted Analysis""," +
                @"""OverallAssessment"":""High risk""," +
                @"""Dependencies"":[],""Impacts"":[]}" +
                "\nAnalysis complete.";

            var result = _parser.Parse(rawResponse);

            Assert.IsNotNull(result);
            Assert.AreEqual(
                "Extracted Analysis",
                result.AnalysisTitle);
            Assert.AreEqual(
                "High risk",
                result.OverallAssessment);
        }

        [TestMethod]
        public void Parse_MalformedJson_ThrowsInvalidOperationException()
        {
            var rawResponse =
                @"{""AnalysisTitle"": ""Broken Analysis"" ";

            var exception =
                Assert.ThrowsException<InvalidOperationException>(
                    () => _parser.Parse(rawResponse));

            Assert.AreEqual(
                "AI dependency and impact response was not valid JSON.",
                exception.Message);

            Assert.IsNotNull(exception.InnerException);
        }

        [TestMethod]
        public void Parse_JsonNull_ReturnsNull()
        {
            var result = _parser.Parse("null");

            Assert.IsNull(result);
        }
    }
}
