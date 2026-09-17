using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services;
using Legacy2Modern.Business.Services.AI;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

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

                var response = analysisService.Analyze();

                lblOverallAssessment.Text =
                    Server.HtmlEncode(
                        response.OverallAssessment);

                lblRecommendedApproach.Text =
                    Server.HtmlEncode(
                        response.RecommendedApproach);

                lblTargetArchitecture.Text =
                    Server.HtmlEncode(
                        response.TargetArchitecture);

                DisplayRecommendations(
                    response.Recommendations);

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

                phRecommendations.Controls.Clear();

                lblRecommendations.Text =
                    "Please check the AI provider configuration " +
                    "and try again.";
            }
        }

        private void DisplayRecommendations(
    System.Collections.Generic.List<ModernizationRecommendation> recommendations)
        {
            phRecommendations.Controls.Clear();

            if (recommendations == null ||
                recommendations.Count == 0)
            {
                var emptyMessage =
                    new LiteralControl(
                        "<div class='analysis-section'>" +
                        "No AI recommendations were returned." +
                        "</div>");

                phRecommendations.Controls.Add(emptyMessage);

                return;
            }

            foreach (var recommendation in recommendations)
            {
                var card =
                    new Panel();

                card.CssClass = "recommendation-card";

                var header =
                    new Panel();

                header.CssClass = "recommendation-header";

                var finding =
                    new LiteralControl(
                        "<h3>Finding " +
                        Server.HtmlEncode(recommendation.FindingId) +
                        "</h3>");

                header.Controls.Add(finding);

                card.Controls.Add(header);


                var actionSection =
                    new Panel();

                actionSection.CssClass = "recommendation-item";

                actionSection.Controls.Add(
                    new LiteralControl(
                        "<strong>Recommended Action</strong>"));

                actionSection.Controls.Add(
                    new LiteralControl(
                        "<div class='recommendation-content'>" +
                        Server.HtmlEncode(
                            recommendation.RecommendedAction) +
                        "</div>"));

                card.Controls.Add(actionSection);


                var reasoningSection =
                    new Panel();

                reasoningSection.CssClass = "recommendation-item";

                reasoningSection.Controls.Add(
                    new LiteralControl(
                        "<strong>Reasoning</strong>"));

                reasoningSection.Controls.Add(
                    new LiteralControl(
                        "<div class='recommendation-content'>" +
                        Server.HtmlEncode(
                            recommendation.Reasoning) +
                        "</div>"));

                card.Controls.Add(reasoningSection);


                var meta =
                    new Panel();

                meta.CssClass = "recommendation-meta";

                meta.Controls.Add(
                    new LiteralControl(
                        "<div class='meta-item'>" +
                        "<span class='meta-label'>Risk:</span> " +
                        Server.HtmlEncode(
                            recommendation.Risk) +
                        "</div>"));

                meta.Controls.Add(
                    new LiteralControl(
                        "<div class='meta-item'>" +
                        "<span class='meta-label'>Complexity:</span> " +
                        Server.HtmlEncode(
                            recommendation.Complexity) +
                        "</div>"));

                meta.Controls.Add(
                    new LiteralControl(
                        "<div class='meta-item'>" +
                        "<span class='meta-label'>Confidence:</span> " +
                        recommendation.Confidence.ToString("P0") +
                        "</div>"));

                card.Controls.Add(meta);


                phRecommendations.Controls.Add(card);
            }
        }
    }
}