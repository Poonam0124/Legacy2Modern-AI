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
                stream = false
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

                var planningResponse =
                    _parser.Parse(
                        ollamaResponse.response);

                var validation =
                    _validator.Validate(
                        planningResponse);

                if (!validation.IsValid)
                {
                    throw new InvalidOperationException(
                        "AI planning response validation failed: " +
                        validation.ErrorMessage);
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