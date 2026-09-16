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

                var providerFactory =
                    new AIProviderFactory();

                var provider =
                    providerFactory.Create(
                        configuration);

                var aiService =
                    new ModernizationAIService(
                        provider);

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

                html.Append(
                    "<p><strong>Overall Assessment:</strong></p>");

                html.Append(
                    "<p>" +
                    Server.HtmlEncode(
                        response.OverallAssessment) +
                    "</p>");

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