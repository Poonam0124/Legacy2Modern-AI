using System;
using Newtonsoft.Json;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationDependencyImpactResponseParser
        : IModernizationDependencyImpactResponseParser
    {
        public ModernizationDependencyImpactAnalysis Parse(
            string rawResponse)
        {
            if (string.IsNullOrWhiteSpace(rawResponse))
            {
                throw new InvalidOperationException(
                    "AI dependency and impact response is empty.");
            }

            var json =
                ExtractJson(rawResponse);

            try
            {
                return JsonConvert.DeserializeObject
                    <ModernizationDependencyImpactAnalysis>(
                        json);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException(
                    "AI dependency and impact response was not valid JSON.",
                    ex);
            }
        }

        private string ExtractJson(string rawResponse)
        {
            var json =
                rawResponse.Trim();

            if (json.StartsWith("```"))
            {
                var firstNewLine =
                    json.IndexOf('\n');

                var lastFence =
                    json.LastIndexOf("```");

                if (firstNewLine >= 0 &&
                    lastFence > firstNewLine)
                {
                    json = json.Substring(
                        firstNewLine + 1,
                        lastFence - firstNewLine - 1);
                }
            }

            json = json.Trim();

            var firstBrace =
                json.IndexOf('{');

            var lastBrace =
                json.LastIndexOf('}');

            if (firstBrace >= 0 &&
                lastBrace > firstBrace)
            {
                json = json.Substring(
                    firstBrace,
                    lastBrace - firstBrace + 1);
            }

            return json.Trim();
        }
    }
}