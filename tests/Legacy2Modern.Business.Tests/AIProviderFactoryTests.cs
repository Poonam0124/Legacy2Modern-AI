
using System;
using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services.AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Legacy2Modern.Business.Tests
{
    [TestClass]
    public class AIProviderFactoryTests
    {
        private readonly AIProviderFactory _factory =
            new AIProviderFactory();

        [TestMethod]
        public void Create_NullConfiguration_ThrowsArgumentNullException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(
                () => _factory.Create(null));

            Assert.AreEqual("configuration", exception.ParamName);
        }

        [TestMethod]
        public void Create_EmptyProviderName_ThrowsArgumentException()
        {
            var configuration = new AIProviderConfiguration
            {
                ProviderName = ""
            };

            var exception = Assert.ThrowsException<ArgumentException>(
                () => _factory.Create(configuration));

            Assert.AreEqual(
                "AI provider name is required.",
                exception.Message);
        }

        [TestMethod]
        public void Create_WhitespaceProviderName_ThrowsArgumentException()
        {
            var configuration = new AIProviderConfiguration
            {
                ProviderName = "   "
            };

            Assert.ThrowsException<ArgumentException>(
                () => _factory.Create(configuration));
        }

        [TestMethod]
        public void Create_OllamaProviderName_ReturnsOllamaProvider()
        {
            var configuration = new AIProviderConfiguration
            {
                ProviderName = "Ollama",
                Endpoint = "http://localhost:11434",
                ModelName = "test-model",
                TimeoutSeconds = 10
            };

            var provider = _factory.Create(configuration);

            Assert.IsNotNull(provider);
            Assert.IsInstanceOfType(
                provider,
                typeof(OllamaAIProvider));
        }

        [TestMethod]
        public void Create_OllamaProviderNameIsCaseInsensitive_ReturnsOllamaProvider()
        {
            var configuration = new AIProviderConfiguration
            {
                ProviderName = "ollama"
            };

            var provider = _factory.Create(configuration);

            Assert.IsInstanceOfType(
                provider,
                typeof(OllamaAIProvider));
        }

        [TestMethod]
        public void Create_UnsupportedProviderName_ThrowsNotSupportedException()
        {
            var configuration = new AIProviderConfiguration
            {
                ProviderName = "UnsupportedProvider"
            };

            var exception = Assert.ThrowsException<NotSupportedException>(
                () => _factory.Create(configuration));

            Assert.AreEqual(
                "AI provider 'UnsupportedProvider' is not supported.",
                exception.Message);
        }
    }
}
