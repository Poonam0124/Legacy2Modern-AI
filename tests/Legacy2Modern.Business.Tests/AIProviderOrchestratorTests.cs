
using System;
using System.Collections.Generic;
using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services.AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Legacy2Modern.Business.Tests
{
    [TestClass]
    public class AIProviderOrchestratorTests
    {
        [TestMethod]
        public void Constructor_NullPrimaryProvider_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(
                () => new AIProviderOrchestrator(
                    null,
                    new TestFallbackProvider(),
                    new AIFallbackConfiguration()));

            Assert.AreEqual("primaryProvider", exception.ParamName);
        }

        [TestMethod]
        public void Constructor_NullFallbackProvider_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(
                () => new AIProviderOrchestrator(
                    new TestProvider(),
                    null,
                    new AIFallbackConfiguration()));

            Assert.AreEqual("fallbackProvider", exception.ParamName);
        }

        [TestMethod]
        public void Constructor_NullConfiguration_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(
                () => new AIProviderOrchestrator(
                    new TestProvider(),
                    new TestFallbackProvider(),
                    null));

            Assert.AreEqual("fallbackConfiguration", exception.ParamName);
        }

        [TestMethod]
        public void Analyze_NullRequest_ThrowsArgumentNullException()
        {
            var orchestrator = CreateOrchestrator(
                new TestProvider(),
                new TestFallbackProvider(),
                false);

            var exception = Assert.ThrowsException<ArgumentNullException>(
                () => orchestrator.Analyze(null));

            Assert.AreEqual("request", exception.ParamName);
        }

        [TestMethod]
        public void Analyze_PrimarySucceeds_ReturnsPrimaryResponse()
        {
            var request = new ModernizationAnalysisRequest();
            var expectedResponse = new ModernizationAnalysisResponse();

            var primary = new TestProvider
            {
                ResponseToReturn = expectedResponse
            };

            var fallback = new TestFallbackProvider();

            var orchestrator = CreateOrchestrator(
                primary,
                fallback,
                true);

            var actualResponse = orchestrator.Analyze(request);

            Assert.AreSame(expectedResponse, actualResponse);
            Assert.AreEqual(1, primary.CallCount);
            Assert.AreEqual(0, fallback.CallCount);
            Assert.AreSame(request, primary.ReceivedRequest);
        }

        [TestMethod]
        public void Analyze_PrimaryFailsFallbackDisabled_RethrowsPrimaryException()
        {
            var primaryException = new InvalidOperationException(
                "Primary provider failed.");

            var primary = new TestProvider
            {
                ExceptionToThrow = primaryException
            };

            var fallback = new TestFallbackProvider();

            var orchestrator = CreateOrchestrator(
                primary,
                fallback,
                false);

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => orchestrator.Analyze(
                    new ModernizationAnalysisRequest()));

            Assert.AreSame(primaryException, exception);
            Assert.AreEqual(0, fallback.CallCount);
        }

        [TestMethod]
        public void Analyze_PrimaryFailsFallbackEnabled_ReturnsFallbackResponse()
        {
            var request = new ModernizationAnalysisRequest();
            var fallbackResponse = new ModernizationAnalysisResponse();

            var primary = new TestProvider
            {
                ExceptionToThrow = new InvalidOperationException(
                    "Primary provider failed.")
            };

            var fallback = new TestFallbackProvider
            {
                ResponseToReturn = fallbackResponse
            };

            var orchestrator = CreateOrchestrator(
                primary,
                fallback,
                true);

            var actualResponse = orchestrator.Analyze(request);

            Assert.AreSame(fallbackResponse, actualResponse);
            Assert.AreEqual(1, primary.CallCount);
            Assert.AreEqual(1, fallback.CallCount);
            Assert.AreSame(request, fallback.ReceivedRequest);
        }

        [TestMethod]
        public void Analyze_BothProvidersFail_ThrowsAggregateFailure()
        {
            var primaryException = new InvalidOperationException(
                "Primary provider failed.");

            var fallbackException = new InvalidOperationException(
                "Fallback provider failed.");

            var primary = new TestProvider
            {
                ExceptionToThrow = primaryException
            };

            var fallback = new TestFallbackProvider
            {
                ExceptionToThrow = fallbackException
            };

            var orchestrator = CreateOrchestrator(
                primary,
                fallback,
                true);

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => orchestrator.Analyze(
                    new ModernizationAnalysisRequest()));

            Assert.AreEqual(
                "Both the primary AI provider and fallback AI provider failed.",
                exception.Message);

            var aggregateException =
                exception.InnerException as AggregateException;

            Assert.IsNotNull(aggregateException);
            Assert.AreEqual(2, aggregateException.InnerExceptions.Count);
            Assert.AreSame(
                primaryException,
                aggregateException.InnerExceptions[0]);
            Assert.AreSame(
                fallbackException,
                aggregateException.InnerExceptions[1]);
        }


        [TestMethod]
        public void Analyze_PrimaryFailsFallbackEnabled_PassesSameRequestToBothProviders()
        {
            var request = new ModernizationAnalysisRequest();

            var primary = new TestProvider
            {
                ExceptionToThrow = new InvalidOperationException(
                    "Primary provider failed.")
            };

            var fallback = new TestFallbackProvider
            {
                ResponseToReturn = new ModernizationAnalysisResponse()
            };

            var orchestrator = CreateOrchestrator(
                primary,
                fallback,
                true);

            orchestrator.Analyze(request);

            Assert.AreSame(request, primary.ReceivedRequest);
            Assert.AreSame(request, fallback.ReceivedRequest);
            Assert.AreEqual(1, primary.CallCount);
            Assert.AreEqual(1, fallback.CallCount);
        }


        private static AIProviderOrchestrator CreateOrchestrator(
            IAIProvider primary,
            IAIProviderFallback fallback,
            bool fallbackEnabled)
        {
            return new AIProviderOrchestrator(
                primary,
                fallback,
                new AIFallbackConfiguration
                {
                    Enabled = fallbackEnabled
                });
        }

        private sealed class TestProvider : IAIProvider
        {
            public ModernizationAnalysisResponse ResponseToReturn
            {
                get;
                set;
            }

            public Exception ExceptionToThrow
            {
                get;
                set;
            }

            public int CallCount
            {
                get;
                private set;
            }

            public ModernizationAnalysisRequest ReceivedRequest
            {
                get;
                private set;
            }

            public ModernizationAnalysisResponse Analyze(
                ModernizationAnalysisRequest request)
            {
                CallCount++;
                ReceivedRequest = request;

                if (ExceptionToThrow != null)
                    throw ExceptionToThrow;

                return ResponseToReturn;
            }
        }

        private sealed class TestFallbackProvider : IAIProviderFallback
        {
            public ModernizationAnalysisResponse ResponseToReturn
            {
                get;
                set;
            }

            public Exception ExceptionToThrow
            {
                get;
                set;
            }

            public int CallCount
            {
                get;
                private set;
            }

            public ModernizationAnalysisRequest ReceivedRequest
            {
                get;
                private set;
            }

            public ModernizationAnalysisResponse Analyze(
                ModernizationAnalysisRequest request)
            {
                CallCount++;
                ReceivedRequest = request;

                if (ExceptionToThrow != null)
                    throw ExceptionToThrow;

                return ResponseToReturn;
            }
        }
    }
}
