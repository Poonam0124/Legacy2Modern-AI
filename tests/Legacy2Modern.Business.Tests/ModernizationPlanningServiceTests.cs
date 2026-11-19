
using System;
using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services.AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Legacy2Modern.Business.Tests
{
    [TestClass]
    public class ModernizationPlanningServiceTests
    {
        [TestMethod]
        public void CreatePlan_NullContext_ThrowsArgumentNullException()
        {
            var requestBuilder = new TestRequestBuilder();
            var planningProvider = new TestPlanningProvider();

            var service = new ModernizationPlanningService(
                requestBuilder,
                planningProvider);

            var exception = Assert.ThrowsException<ArgumentNullException>(
                () => service.CreatePlan(null));

            Assert.AreEqual("context", exception.ParamName);
        }

        [TestMethod]
        public void CreatePlan_ValidContext_PassesContextToRequestBuilder()
        {
            var context = new ModernizationPlanningContext();
            var request = new ModernizationPlanningRequest();

            var requestBuilder = new TestRequestBuilder
            {
                RequestToReturn = request
            };

            var planningProvider = new TestPlanningProvider
            {
                ResponseToReturn = new ModernizationPlanningResponse()
            };

            var service = new ModernizationPlanningService(
                requestBuilder,
                planningProvider);

            service.CreatePlan(context);

            Assert.AreSame(context, requestBuilder.ReceivedContext);
        }

        [TestMethod]
        public void CreatePlan_ProviderReturnsResponse_ReturnsSameResponse()
        {
            var context = new ModernizationPlanningContext();
            var request = new ModernizationPlanningRequest();
            var expectedResponse = new ModernizationPlanningResponse();

            var requestBuilder = new TestRequestBuilder
            {
                RequestToReturn = request
            };

            var planningProvider = new TestPlanningProvider
            {
                ResponseToReturn = expectedResponse
            };

            var service = new ModernizationPlanningService(
                requestBuilder,
                planningProvider);

            var actualResponse = service.CreatePlan(context);

            Assert.AreSame(request, planningProvider.ReceivedRequest);
            Assert.AreSame(expectedResponse, actualResponse);
        }

        [TestMethod]
        public void CreatePlan_ProviderReturnsNull_ThrowsInvalidOperationException()
        {
            var context = new ModernizationPlanningContext();
            var request = new ModernizationPlanningRequest();

            var requestBuilder = new TestRequestBuilder
            {
                RequestToReturn = request
            };

            var planningProvider = new TestPlanningProvider
            {
                ResponseToReturn = null
            };

            var service = new ModernizationPlanningService(
                requestBuilder,
                planningProvider);

            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => service.CreatePlan(context));

            Assert.AreEqual(
                "AI planning provider returned an empty response.",
                exception.Message);
        }

        [TestMethod]
        public void CreatePlan_RequestBuilderThrows_DoesNotCallProvider()
        {
            var context = new ModernizationPlanningContext();
            var expectedException = new InvalidOperationException(
                "Request building failed.");

            var requestBuilder = new TestRequestBuilder
            {
                ExceptionToThrow = expectedException
            };

            var planningProvider = new TestPlanningProvider();

            var service = new ModernizationPlanningService(
                requestBuilder,
                planningProvider);

            var actualException =
                Assert.ThrowsException<InvalidOperationException>(
                    () => service.CreatePlan(context));

            Assert.AreSame(expectedException, actualException);
            Assert.AreEqual(0, planningProvider.CallCount);
        }

        [TestMethod]
        public void CreatePlan_ProviderThrows_PropagatesOriginalException()
        {
            var context = new ModernizationPlanningContext();
            var request = new ModernizationPlanningRequest();

            var expectedException = new InvalidOperationException(
                "Planning provider failed.");

            var requestBuilder = new TestRequestBuilder
            {
                RequestToReturn = request
            };

            var planningProvider = new TestPlanningProvider
            {
                ExceptionToThrow = expectedException
            };

            var service = new ModernizationPlanningService(
                requestBuilder,
                planningProvider);

            var actualException =
                Assert.ThrowsException<InvalidOperationException>(
                    () => service.CreatePlan(context));

            Assert.AreSame(expectedException, actualException);
            Assert.AreSame(request, planningProvider.ReceivedRequest);
        }

        private sealed class TestRequestBuilder
            : IModernizationPlanningRequestBuilder
        {
            public ModernizationPlanningContext ReceivedContext
            {
                get;
                private set;
            }

            public ModernizationPlanningRequest RequestToReturn
            {
                get;
                set;
            }

            public Exception ExceptionToThrow
            {
                get;
                set;
            }

            public ModernizationPlanningRequest Build(
                ModernizationPlanningContext context)
            {
                ReceivedContext = context;

                if (ExceptionToThrow != null)
                    throw ExceptionToThrow;

                return RequestToReturn;
            }
        }

        private sealed class TestPlanningProvider
            : IAIPlanningProvider
        {
            public ModernizationPlanningRequest ReceivedRequest
            {
                get;
                private set;
            }

            public ModernizationPlanningResponse ResponseToReturn
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

            public ModernizationPlanningResponse CreatePlan(
                ModernizationPlanningRequest request)
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
