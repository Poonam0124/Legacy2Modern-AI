
using System;
using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services.AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Legacy2Modern.Business.Tests
{
    [TestClass]
    public class ModernizationDependencyImpactAnalysisServiceTests
    {
        [TestMethod]
        public void Constructor_NullRequestBuilder_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(
                () => new ModernizationDependencyImpactAnalysisService(
                    null,
                    new TestProvider(),
                    new TestValidator()));

            Assert.AreEqual("requestBuilder", exception.ParamName);
        }

        [TestMethod]
        public void Constructor_NullProvider_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(
                () => new ModernizationDependencyImpactAnalysisService(
                    new TestRequestBuilder(),
                    null,
                    new TestValidator()));

            Assert.AreEqual("provider", exception.ParamName);
        }

        [TestMethod]
        public void Constructor_NullValidator_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(
                () => new ModernizationDependencyImpactAnalysisService(
                    new TestRequestBuilder(),
                    new TestProvider(),
                    null));

            Assert.AreEqual("validator", exception.ParamName);
        }

        [TestMethod]
        public void Analyze_NullContext_ThrowsArgumentNullException()
        {
            var service = CreateService(
                new TestRequestBuilder(),
                new TestProvider(),
                new TestValidator());

            var exception = Assert.ThrowsException<ArgumentNullException>(
                () => service.Analyze(null));

            Assert.AreEqual("context", exception.ParamName);
        }

        [TestMethod]
        public void Analyze_ProviderReturnsNull_ThrowsInvalidOperationException()
        {
            var context = new ModernizationDependencyImpactContext();

            var requestBuilder = new TestRequestBuilder
            {
                RequestToReturn = new ModernizationDependencyImpactRequest()
            };

            var provider = new TestProvider
            {
                AnalysisToReturn = null
            };

            var validator = new TestValidator();

            var service = CreateService(
                requestBuilder,
                provider,
                validator);

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => service.Analyze(context));

            Assert.AreEqual(
                "Dependency and impact analysis response is null.",
                exception.Message);

            Assert.AreEqual(0, validator.CallCount);
        }

        [TestMethod]
        public void Analyze_ValidatorReturnsNull_ThrowsInvalidOperationException()
        {
            var context = new ModernizationDependencyImpactContext();
            var analysis = new ModernizationDependencyImpactAnalysis();

            var service = CreateService(
                new TestRequestBuilder
                {
                    RequestToReturn = new ModernizationDependencyImpactRequest()
                },
                new TestProvider
                {
                    AnalysisToReturn = analysis
                },
                new TestValidator
                {
                    ValidationToReturn = null
                });

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => service.Analyze(context));

            StringAssert.Contains(
                exception.Message,
                "Dependency and impact analysis validation failed: Validation returned no result.");
        }

        [TestMethod]
        public void Analyze_InvalidValidation_ThrowsInvalidOperationException()
        {
            var context = new ModernizationDependencyImpactContext();
            var analysis = new ModernizationDependencyImpactAnalysis();

            var service = CreateService(
                new TestRequestBuilder
                {
                    RequestToReturn = new ModernizationDependencyImpactRequest()
                },
                new TestProvider
                {
                    AnalysisToReturn = analysis
                },
                new TestValidator
                {
                    ValidationToReturn =
                        new ModernizationDependencyImpactValidationResult
                        {
                            IsValid = false,
                            ErrorMessage = "Test validation error."
                        }
                });

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => service.Analyze(context));

            StringAssert.Contains(
                exception.Message,
                "Dependency and impact analysis validation failed: Test validation error.");
        }

        [TestMethod]
        public void Analyze_ValidAnalysis_ReturnsSameAnalysis()
        {
            var context = new ModernizationDependencyImpactContext();
            var request = new ModernizationDependencyImpactRequest();
            var analysis = new ModernizationDependencyImpactAnalysis();

            var requestBuilder = new TestRequestBuilder
            {
                RequestToReturn = request
            };

            var provider = new TestProvider
            {
                AnalysisToReturn = analysis
            };

            var validator = new TestValidator
            {
                ValidationToReturn =
                    new ModernizationDependencyImpactValidationResult
                    {
                        IsValid = true,
                        ErrorMessage = null
                    }
            };

            var service = CreateService(
                requestBuilder,
                provider,
                validator);

            var actual = service.Analyze(context);

            Assert.AreSame(context, requestBuilder.ReceivedContext);
            Assert.AreSame(request, provider.ReceivedRequest);
            Assert.AreSame(analysis, validator.ReceivedAnalysis);
            Assert.AreSame(context, validator.ReceivedContext);
            Assert.AreSame(analysis, actual);
        }

        [TestMethod]
        public void Analyze_RequestBuilderThrows_ProviderAndValidatorAreNotCalled()
        {
            var context = new ModernizationDependencyImpactContext();

            var expectedException = new InvalidOperationException(
                "Request building failed.");

            var requestBuilder = new TestRequestBuilder
            {
                ExceptionToThrow = expectedException
            };

            var provider = new TestProvider();
            var validator = new TestValidator();

            var service = CreateService(
                requestBuilder,
                provider,
                validator);

            var actualException =
                Assert.ThrowsException<InvalidOperationException>(
                    () => service.Analyze(context));

            Assert.AreSame(expectedException, actualException);
            Assert.AreEqual(0, provider.CallCount);
            Assert.AreEqual(0, validator.CallCount);
        }

        [TestMethod]
        public void Analyze_ProviderThrows_ValidatorIsNotCalled()
        {
            var context = new ModernizationDependencyImpactContext();
            var request = new ModernizationDependencyImpactRequest();

            var expectedException = new InvalidOperationException(
                "Dependency-impact provider failed.");

            var requestBuilder = new TestRequestBuilder
            {
                RequestToReturn = request
            };

            var provider = new TestProvider
            {
                ExceptionToThrow = expectedException
            };

            var validator = new TestValidator();

            var service = CreateService(
                requestBuilder,
                provider,
                validator);

            var actualException =
                Assert.ThrowsException<InvalidOperationException>(
                    () => service.Analyze(context));

            Assert.AreSame(expectedException, actualException);
            Assert.AreSame(context, requestBuilder.ReceivedContext);
            Assert.AreSame(request, provider.ReceivedRequest);
            Assert.AreEqual(0, validator.CallCount);
        }

        private static ModernizationDependencyImpactAnalysisService CreateService(
            IModernizationDependencyImpactRequestBuilder requestBuilder,
            IAIDependencyImpactProvider provider,
            IModernizationDependencyImpactResponseValidator validator)
        {
            return new ModernizationDependencyImpactAnalysisService(
                requestBuilder,
                provider,
                validator);
        }

        private sealed class TestRequestBuilder
            : IModernizationDependencyImpactRequestBuilder
        {
            public ModernizationDependencyImpactContext ReceivedContext
            {
                get;
                private set;
            }

            public ModernizationDependencyImpactRequest RequestToReturn
            {
                get;
                set;
            }

            public Exception ExceptionToThrow
            {
                get;
                set;
            }

            public ModernizationDependencyImpactRequest Build(
                ModernizationDependencyImpactContext context)
            {
                ReceivedContext = context;

                if (ExceptionToThrow != null)
                    throw ExceptionToThrow;

                return RequestToReturn;
            }
        }

        private sealed class TestProvider : IAIDependencyImpactProvider
        {
            public ModernizationDependencyImpactRequest ReceivedRequest
            {
                get;
                private set;
            }

            public ModernizationDependencyImpactAnalysis AnalysisToReturn
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

            public ModernizationDependencyImpactAnalysis Analyze(
                ModernizationDependencyImpactRequest request)
            {
                CallCount++;
                ReceivedRequest = request;

                if (ExceptionToThrow != null)
                    throw ExceptionToThrow;

                return AnalysisToReturn;
            }
        }

        private sealed class TestValidator
            : IModernizationDependencyImpactResponseValidator
        {
            public ModernizationDependencyImpactAnalysis ReceivedAnalysis
            {
                get;
                private set;
            }

            public ModernizationDependencyImpactContext ReceivedContext
            {
                get;
                private set;
            }

            public ModernizationDependencyImpactValidationResult ValidationToReturn
            {
                get;
                set;
            }

            public int CallCount
            {
                get;
                private set;
            }

            public ModernizationDependencyImpactValidationResult Validate(
                ModernizationDependencyImpactAnalysis analysis)
            {
                CallCount++;
                ReceivedAnalysis = analysis;
                return ValidationToReturn;
            }

            public ModernizationDependencyImpactValidationResult Validate(
                ModernizationDependencyImpactAnalysis analysis,
                ModernizationDependencyImpactContext context)
            {
                CallCount++;
                ReceivedAnalysis = analysis;
                ReceivedContext = context;
                return ValidationToReturn;
            }
        }
    }
}
