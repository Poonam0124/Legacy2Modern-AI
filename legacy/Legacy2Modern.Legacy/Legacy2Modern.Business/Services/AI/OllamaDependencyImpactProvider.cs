using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class OllamaDependencyImpactProvider
        : IAIDependencyImpactProvider
    {
        private readonly AIProviderConfiguration _configuration;

        private readonly
            IModernizationDependencyImpactResponseParser
            _parser;

        private readonly
            IModernizationDependencyImpactResponseValidator
            _validator;

        public OllamaDependencyImpactProvider(
            AIProviderConfiguration configuration,
            IModernizationDependencyImpactResponseParser parser,
            IModernizationDependencyImpactResponseValidator validator)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException(
                    "configuration");

            _parser = parser
                ?? throw new ArgumentNullException(
                    "parser");

            _validator = validator
                ?? throw new ArgumentNullException(
                    "validator");
        }

        public ModernizationDependencyImpactAnalysis Analyze(
            ModernizationDependencyImpactRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.Prompt == null)
            {
                throw new InvalidOperationException(
                    "Dependency and impact analysis prompt is required.");
            }

            var payload = new
            {
                model = _configuration.ModelName,

                prompt = request.Prompt.UserInstruction,

                system = request.Prompt.SystemInstruction,

                stream = false,

                format = new
                {
                    type = "object",

                    properties = new
                    {
                        AnalysisTitle = new
                        {
                            type = "string"
                        },

                        OverallAssessment = new
                        {
                            type = "string"
                        },

                        Dependencies = new
                        {
                            type = "array",

                            items = new
                            {
                                type = "object",

                                properties = new
                                {
                                    DependencyId = new
                                    {
                                        type = "string"
                                    },

                                    SourceId = new
                                    {
                                        type = "string"
                                    },

                                    TargetId = new
                                    {
                                        type = "string"
                                    },

                                    DependencyType = new
                                    {
                                        type = "string"
                                    },

                                    Description = new
                                    {
                                        type = "string"
                                    },

                                    Impact = new
                                    {
                                        type = "string"
                                    },

                                    AffectedComponents = new
                                    {
                                        type = "array",

                                        items = new
                                        {
                                            type = "string"
                                        }
                                    },

                                    Reasons = new
                                    {
                                        type = "array",

                                        items = new
                                        {
                                            type = "string"
                                        }
                                    }
                                },

                                required = new[]
                                {
                                    "DependencyId",
                                    "SourceId",
                                    "TargetId",
                                    "DependencyType",
                                    "Description",
                                    "Impact",
                                    "AffectedComponents",
                                    "Reasons"
                                }
                            }
                        },

                        Impacts = new
                        {
                            type = "array",

                            items = new
                            {
                                type = "object",

                                properties = new
                                {
                                    ImpactId = new
                                    {
                                        type = "string"
                                    },

                                    FindingId = new
                                    {
                                        type = "string"
                                    },

                                    ImpactArea = new
                                    {
                                        type = "string"
                                    },

                                    ImpactLevel = new
                                    {
                                        type = "string"
                                    },

                                    Description = new
                                    {
                                        type = "string"
                                    },

                                    AffectedComponents = new
                                    {
                                        type = "array",

                                        items = new
                                        {
                                            type = "string"
                                        }
                                    },

                                    Risks = new
                                    {
                                        type = "array",

                                        items = new
                                        {
                                            type = "string"
                                        }
                                    },

                                    Mitigations = new
                                    {
                                        type = "array",

                                        items = new
                                        {
                                            type = "string"
                                        }
                                    }
                                },

                                required = new[]
                                {
                                    "ImpactId",
                                    "FindingId",
                                    "ImpactArea",
                                    "ImpactLevel",
                                    "Description",
                                    "AffectedComponents",
                                    "Risks",
                                    "Mitigations"
                                }
                            }
                        }
                    },

                    required = new[]
                    {
                        "AnalysisTitle",
                        "OverallAssessment",
                        "Dependencies",
                        "Impacts"
                    }
                }
            };

            var jsonPayload =
                JsonConvert.SerializeObject(payload);

            using (var client = new HttpClient())
            {
                client.Timeout =
                    TimeSpan.FromSeconds(
                        _configuration.TimeoutSeconds);

                var content =
                    new StringContent(
                        jsonPayload,
                        Encoding.UTF8,
                        "application/json");

                var response =
                    client.PostAsync(
                        _configuration.Endpoint,
                        content)
                    .GetAwaiter()
                    .GetResult();

                response.EnsureSuccessStatusCode();

                var responseContent =
                    response.Content
                        .ReadAsStringAsync()
                        .GetAwaiter()
                        .GetResult();

                var ollamaResponse =
                    JsonConvert.DeserializeObject
                        <OllamaGenerateResponse>(
                            responseContent);

                if (ollamaResponse == null ||
                    string.IsNullOrWhiteSpace(
                        ollamaResponse.response))
                {
                    throw new InvalidOperationException(
                        "Ollama returned an empty dependency and impact response.");
                }

                var rawResponse =
                    ollamaResponse.response;

                var analysis =
                    _parser.Parse(rawResponse);

                if (analysis == null)
                {
                    throw new InvalidOperationException(
                        "Parsed dependency and impact analysis is null.");
                }

                var validation =
                    _validator.Validate(analysis);

                if (!validation.IsValid)
                {
                    throw new InvalidOperationException(
                        "AI dependency and impact response validation failed: " +
                        validation.ErrorMessage);
                }

                return analysis;
            }
        }

        private class OllamaGenerateResponse
        {
            public string response { get; set; }
        }
    }
}