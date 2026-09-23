using System;
using Newtonsoft.Json;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class ModernizationPlanningResponseParser
        : IModernizationPlanningResponseParser
    {
        public ModernizationPlanningResponse Parse(
            string rawResponse)
        {
            if (string.IsNullOrWhiteSpace(rawResponse))
            {
                throw new InvalidOperationException(
                    "AI planning response was empty.");
            }

            var json = rawResponse.Trim();

            if (json.StartsWith("```"))
            {
                var firstNewLine = json.IndexOf('\n');

                if (firstNewLine >= 0)
                {
                    json = json.Substring(
                        firstNewLine + 1);
                }

                var closingFence = json.LastIndexOf("```");

                if (closingFence >= 0)
                {
                    json = json.Substring(
                        0,
                        closingFence);
                }
            }

            try
            {
                var response =
                    JsonConvert.DeserializeObject
                    <ModernizationPlanningResponse>(json);

                if (response == null)
                {
                    throw new InvalidOperationException(
                        "Unable to parse AI planning response.");
                }

                return response;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException(
                    "AI planning response was not valid JSON.",
                    ex);
            }
        }
    }
}