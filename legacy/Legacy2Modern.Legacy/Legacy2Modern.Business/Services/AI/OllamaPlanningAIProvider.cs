using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class OllamaPlanningAIProvider
        : IAIPlanningProvider
    {
        private readonly AIProviderConfiguration _configuration;
        private readonly IModernizationPlanningResponseParser _parser;
        private readonly IModernizationPlanningResponseValidator _validator;

        public OllamaPlanningAIProvider(
            AIProviderConfiguration configuration,
            IModernizationPlanningResponseParser parser,
            IModernizationPlanningResponseValidator validator)
        {
            _configuration = configuration;
            _parser = parser;
            _validator = validator;
        }

        public ModernizationPlanningResponse CreatePlan(
            ModernizationPlanningRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.Prompt == null)
            {
                throw new InvalidOperationException(
                    "Planning prompt is required.");
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
                        Plan = new
                        {
                            type = "object",
                            properties = new
                            {
                                PlanTitle = new
                                {
                                    type = "string"
                                },
                                OverallStrategy = new
                                {
                                    type = "string"
                                },
                                TargetArchitecture = new
                                {
                                    type = "string"
                                },
                                Phases = new
                                {
                                    type = "array",
                                    items = new
                                    {
                                        type = "object",
                                        properties = new
                                        {
                                            PhaseNumber = new
                                            {
                                                type = "integer"
                                            },
                                            PhaseName = new
                                            {
                                                type = "string"
                                            },
                                            Objective = new
                                            {
                                                type = "string"
                                            },
                                            Rationale = new
                                            {
                                                type = "string"
                                            }
                                        },
                                        required = new[]
                             {
                                "PhaseNumber",
                                "PhaseName",
                                "Objective",
                                "Rationale"
                            }
                                    }
                                },
                                Items = new
                                {
                                    type = "array",
                                    items = new
                                    {
                                        type = "object",
                                        properties = new
                                        {
                                            PlanItemId = new
                                            {
                                                type = "string"
                                            },
                                            PhaseNumber = new
                                            {
                                                type = "integer"
                                            },
                                            FindingId = new
                                            {
                                                type = "string"
                                            },
                                            Title = new
                                            {
                                                type = "string"
                                            },
                                            RecommendedAction = new
                                            {
                                                type = "string"
                                            },
                                            Reasoning = new
                                            {
                                                type = "string"
                                            },
                                            Priority = new
                                            {
                                                type = "string"
                                            },
                                            Risk = new
                                            {
                                                type = "string"
                                            },
                                            Complexity = new
                                            {
                                                type = "string"
                                            },
                                            Effort = new
                                            {
                                                type = "string"
                                            },
                                            Dependencies = new
                                            {
                                                type = "array",
                                                items = new
                                                {
                                                    type = "string"
                                                }
                                            },
                                            AffectedComponents = new
                                            {
                                                type = "array",
                                                items = new
                                                {
                                                    type = "string"
                                                }
                                            },
                                            ImplementationSteps = new
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
                                "PlanItemId",
                                "PhaseNumber",
                                "FindingId",
                                "Title",
                                "RecommendedAction",
                                "Reasoning",
                                "Priority",
                                "Risk",
                                "Complexity",
                                "Effort",
                                "Dependencies",
                                "AffectedComponents",
                                "ImplementationSteps"
                            }
                                    }
                                }
                            },
                            required = new[]
                 {
                    "PlanTitle",
                    "OverallStrategy",
                    "TargetArchitecture",
                    "Phases",
                    "Items"
                }
                        }
                    },
                    required = new[]
         {
            "Plan"
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
                    JsonConvert.DeserializeObject<OllamaGenerateResponse>(
                        responseContent);

                if (ollamaResponse == null ||
                    string.IsNullOrWhiteSpace(
                        ollamaResponse.response))
                {
                    throw new InvalidOperationException(
                        "Ollama returned an empty planning response.");
                }

                var rawPlanningResponse = ollamaResponse.response;

                var planningResponse =
      _parser.Parse(
          rawPlanningResponse);

                if (planningResponse == null)
                {
                    throw new InvalidOperationException(
                        "Parsed planning response is null. Raw response: " +
                        rawPlanningResponse);
                }

                if (planningResponse.Plan == null)
                {
                    throw new InvalidOperationException(
                        "Parsed planning response does not contain Plan. Raw response: " +
                        rawPlanningResponse);
                }

                var validation =
                    _validator.Validate(
                        planningResponse);
                if (!validation.IsValid)
                {
                    throw new InvalidOperationException(
                        "AI planning response validation failed: " +
                        validation.ErrorMessage +
                        " Raw response: " +
                        rawPlanningResponse);
                }
                planningResponse.ProviderName =
                    "Ollama";

                return planningResponse;
            }
        }

        private class OllamaGenerateResponse
        {
            public string response { get; set; }
        }
    }
}