using Legacy2Modern.Business.Models.AI;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Legacy2Modern.Business.Services.AI
{
    public class OllamaAIProvider : IAIProvider
    {
        private readonly string _endpoint;
        private readonly string _model;
        private readonly int _timeoutSeconds;

        public OllamaAIProvider(
     string endpoint,
     string model,
     int timeoutSeconds = 120)
        {
            _endpoint = endpoint;
            _model = model;
            _timeoutSeconds = timeoutSeconds;
        }

        public ModernizationAnalysisResponse Analyze(
            ModernizationAnalysisRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (request.Prompt == null)
                throw new ArgumentException(
                    "AI prompt is required.");

            var prompt =
                request.Prompt.SystemInstruction
                + Environment.NewLine
                + Environment.NewLine
                + request.Prompt.UserInstruction;

            var ollamaRequest = new
            {
                model = _model,
                prompt = prompt,
                stream = false
            };

            var json =
                JsonConvert.SerializeObject(ollamaRequest);

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(_timeoutSeconds);

                using (var content =
                    new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json"))
                {
                    HttpResponseMessage response;

                    try
                    {
                        response =
                            client.PostAsync(
                                _endpoint,
                                content)
                            .GetAwaiter()
                            .GetResult();
                    }
                    catch (TaskCanceledException ex)
                    {
                        throw new InvalidOperationException(
                            "The AI request timed out or was cancelled. " +
                            "Endpoint: " + _endpoint +
                            ". Timeout: " + _timeoutSeconds + " seconds.",
                            ex);
                    }

                    response.EnsureSuccessStatusCode();

                    var responseJson =
                        response.Content
                            .ReadAsStringAsync()
                            .GetAwaiter()
                            .GetResult();

                    var ollamaResponse =
                        JsonConvert.DeserializeObject<OllamaResponse>(
                            responseJson);

                    if (ollamaResponse == null)
                        throw new InvalidOperationException(
                            "Ollama returned an empty response.");

                    return new ModernizationAnalysisResponse
                    {
                        OverallAssessment =
                            ollamaResponse.Response
                    };
                }
            }
        }

        private class OllamaResponse
        {
            public string Response { get; set; }
        }
    }
}