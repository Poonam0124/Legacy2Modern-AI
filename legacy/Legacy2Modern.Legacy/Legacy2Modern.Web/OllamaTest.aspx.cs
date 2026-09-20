using System;
using System.Configuration;
using System.Text;
using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services;
using Legacy2Modern.Business.Services.AI;

namespace Legacy2Modern.Web
{
    public partial class OllamaTest : System.Web.UI.Page
    {
        protected void btnAnalyze_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                var findingService =
                    new ModernizationFindingService();

                var promptBuilder =
                    new ModernizationPromptBuilder();

                var requestBuilder =
                    new ModernizationAIRequestBuilder(
                        promptBuilder);

                var configuration =
                    new AIProviderConfiguration
                    {
                        ProviderName =
                            ConfigurationManager.AppSettings[
                                "AIProvider"],

                        ModelName =
                            ConfigurationManager.AppSettings[
                                "AIModel"],

                        Endpoint =
                            ConfigurationManager.AppSettings[
                                "AIEndpoint"],

                        TimeoutSeconds =
                            int.Parse(
                                ConfigurationManager.AppSettings[
                                    "AITimeoutSeconds"])
                    };
                var fallbackConfiguration =
                   new AIFallbackConfiguration
                   {
                       Enabled =
                           bool.Parse(
                               System.Configuration.ConfigurationManager
                                   .AppSettings["AIFallbackEnabled"]),

                       ProviderName =
                           System.Configuration.ConfigurationManager
                               .AppSettings["AIFallbackProvider"]
                   };
                var providerFactory = new AIProviderFactory();

                var primaryProvider =
                    providerFactory.Create(configuration);

                var fallbackProvider =
                    new MockModernizationAIService();

                var orchestrator =
                    new AIProviderOrchestrator(
                        primaryProvider,
                        fallbackProvider,
                        fallbackConfiguration);

                var aiService =
                    new ModernizationAIService(orchestrator);

                var analysisService =
                    new ModernizationAnalysisService(
                        findingService,
                        promptBuilder,
                        requestBuilder,
                        aiService);

                var response =
                    analysisService.Analyze();

                var html =
                    new StringBuilder();

                html.Append(
                    "<h3>AI Analysis Result</h3>");

                html.Append("<p><strong>Overall Assessment:</strong></p>");

                html.Append(
                    "<p>" +
                    Server.HtmlEncode(
                        response.OverallAssessment) +
                    "</p>");

                html.Append(
                    "<p><strong>Recommended Approach:</strong></p>");

                html.Append(
                    "<p>" +
                    Server.HtmlEncode(
                        response.RecommendedApproach) +
                    "</p>");

                html.Append(
                    "<p><strong>Target Architecture:</strong></p>");

                html.Append(
                    "<p>" +
                    Server.HtmlEncode(
                        response.TargetArchitecture) +
                    "</p>");

                html.Append(
                    "<p><strong>Recommendations:</strong></p>");

                if (response.Recommendations != null)
                {
                    foreach (var recommendation
                        in response.Recommendations)
                    {
                        html.Append("<hr />");

                        html.Append(
                            "<p><strong>Finding:</strong> " +
                            Server.HtmlEncode(
                                recommendation.FindingId) +
                            "</p>");

                        html.Append(
                            "<p><strong>Action:</strong> " +
                            Server.HtmlEncode(
                                recommendation.RecommendedAction) +
                            "</p>");

                        html.Append(
                            "<p><strong>Reasoning:</strong> " +
                            Server.HtmlEncode(
                                recommendation.Reasoning) +
                            "</p>");

                        html.Append(
                            "<p><strong>Risk:</strong> " +
                            Server.HtmlEncode(
                                recommendation.Risk) +
                            "</p>");

                        html.Append(
                            "<p><strong>Complexity:</strong> " +
                            Server.HtmlEncode(
                                recommendation.Complexity) +
                            "</p>");

                        html.Append(
                            "<p><strong>Confidence:</strong> " +
                            recommendation.Confidence.ToString("0.00") +
                            "</p>");
                    }
                }

                lblResult.Text =
                    html.ToString();
            }
            catch (Exception ex)
            {
                lblResult.Text =
                    "<strong>Error:</strong><br/>" +
                    Server.HtmlEncode(
                        ex.ToString())
                    .Replace(
                        Environment.NewLine,
                        "<br/>");
            }
        }
    }
}