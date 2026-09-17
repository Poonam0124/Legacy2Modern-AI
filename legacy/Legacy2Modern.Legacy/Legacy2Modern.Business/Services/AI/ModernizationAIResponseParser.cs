using System;
using Legacy2Modern.Business.Models.AI;
using Newtonsoft.Json;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationAIResponseParser
        : IModernizationAIResponseParser
    {
        public ModernizationAnalysisResponse Parse(
            string response)
        {
            if (string.IsNullOrWhiteSpace(response))
            {
                throw new ArgumentException(
                    "AI response is empty.");
            }

            var json =
                response.Trim();

            // Remove Markdown code fences if the AI
            // returns JSON wrapped in ```json ... ```
            if (json.StartsWith("```"))
            {
                var firstNewLine =
                    json.IndexOf(
                        Environment.NewLine,
                        StringComparison.Ordinal);

                if (firstNewLine >= 0)
                {
                    json =
                        json.Substring(
                            firstNewLine + Environment.NewLine.Length);
                }

                var closingFence =
                    json.LastIndexOf(
                        "```",
                        StringComparison.Ordinal);

                if (closingFence >= 0)
                {
                    json =
                        json.Substring(
                            0,
                            closingFence);
                }

                json = json.Trim();
            }

            try
            {
                var result =
                    JsonConvert.DeserializeObject<
                        ModernizationAnalysisResponse>(
                            json);

                if (result == null)
                {
                    throw new InvalidOperationException(
                        "AI response could not be parsed.");
                }

                return result;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException(
                    "AI response is not valid JSON.",
                    ex);
            }
        }
    }
}