using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services;
using Legacy2Modern.Business.Services.AI;
using System;

namespace Legacy2Modern.Web
{
    public partial class ModernizationAIAnalysis
        : System.Web.UI.Page
    {
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
        }

        protected void btnRunAnalysis_Click(
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
                            System.Configuration
                                .ConfigurationManager
                                .AppSettings["AIProvider"],

                        ModelName =
                            System.Configuration
                                .ConfigurationManager
                                .AppSettings["AIModel"],

                        Endpoint =
                            System.Configuration
                                .ConfigurationManager
                                .AppSettings["AIEndpoint"],

                        TimeoutSeconds =
                            int.Parse(
                                System.Configuration
                                    .ConfigurationManager
                                    .AppSettings[
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

                lblOverallAssessment.Text =
                    Server.HtmlEncode(
                        response.OverallAssessment);

                lblRecommendedApproach.Text =
                    Server.HtmlEncode(
                        response.RecommendedApproach);

                lblTargetArchitecture.Text =
                    Server.HtmlEncode(
                        response.TargetArchitecture);

                lblRecommendations.Text =
                    "AI analysis completed successfully.";
            }
            catch (Exception ex)
            {
                lblOverallAssessment.Text =
                    "Unable to complete AI analysis.";

                lblRecommendedApproach.Text =
                    Server.HtmlEncode(
                        ex.Message);

                lblTargetArchitecture.Text =
                    string.Empty;

                lblRecommendations.Text =
                    "Please check the AI provider configuration " +
                    "and try again.";
            }
        }
    }
}