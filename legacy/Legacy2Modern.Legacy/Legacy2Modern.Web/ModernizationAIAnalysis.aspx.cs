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

                var configuration = new AIProviderConfiguration
                {
                    ProviderName =
                        System.Configuration.ConfigurationManager
                            .AppSettings["AIProvider"],
                                    ModelName =
                        System.Configuration.ConfigurationManager
                            .AppSettings["AIModel"],
                                    Endpoint =
                        System.Configuration.ConfigurationManager
                            .AppSettings["AIEndpoint"],
                                    TimeoutSeconds =
                        int.Parse(
                            System.Configuration.ConfigurationManager
                                .AppSettings["AITimeoutSeconds"])
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
                    "AI analysis completed successfully. " +
                    response.Recommendations.Count +
                    " recommendations generated.";

                lblRecommendations.CssClass =
                    "analysis-status status-success";

                btnRunAnalysis.Enabled = true;
            }
            catch (Exception ex)
            {
                lblOverallAssessment.Text =
                    "Unable to complete AI analysis.";

                lblRecommendedApproach.Text =
                    string.Empty;

                lblTargetArchitecture.Text =
                    string.Empty;

                phRecommendations.Controls.Clear();

                lblRecommendations.Text =
                    "AI analysis failed: " +
                    Server.HtmlEncode(ex.Message);

                lblRecommendations.CssClass =
                    "analysis-status status-error";

                btnRunAnalysis.Enabled = true;
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

                // -------------------------------------------------
                // Header
                // -------------------------------------------------

                var header =
                    new Panel();

                header.CssClass = "recommendation-header";

                header.Controls.Add(
                    new LiteralControl(
                        "<h3>Finding " +
                        Server.HtmlEncode(
                            recommendation.FindingId) +
                        "</h3>"));

                card.Controls.Add(header);


                // -------------------------------------------------
                // Recommended Action
                // -------------------------------------------------

                var actionSection =
                    new Panel();

                actionSection.CssClass =
                    "recommendation-item";

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


                // -------------------------------------------------
                // Reasoning
                // -------------------------------------------------

                var reasoningSection =
                    new Panel();

                reasoningSection.CssClass =
                    "recommendation-item";

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


                // -------------------------------------------------
                // Affected Components
                // -------------------------------------------------

                var affectedSection =
                    new Panel();

                affectedSection.CssClass =
                    "recommendation-subsection";

                affectedSection.Controls.Add(
                    new LiteralControl(
                        "<strong>Affected Components</strong>"));

                var affectedList =
                    new LiteralControl(
                        "<ul class='recommendation-list'>");

                affectedSection.Controls.Add(
                    affectedList);

                if (recommendation.AffectedComponents != null &&
                    recommendation.AffectedComponents.Count > 0)
                {
                    foreach (var component
                        in recommendation.AffectedComponents)
                    {
                        affectedSection.Controls.Add(
                            new LiteralControl(
                                "<li>" +
                                Server.HtmlEncode(component) +
                                "</li>"));
                    }
                }
                else
                {
                    affectedSection.Controls.Add(
                        new LiteralControl(
                            "<li>No affected components specified.</li>"));
                }

                affectedSection.Controls.Add(
                    new LiteralControl(
                        "</ul>"));

                card.Controls.Add(affectedSection);


                // -------------------------------------------------
                // Implementation Steps
                // -------------------------------------------------

                var stepsSection =
                    new Panel();

                stepsSection.CssClass =
                    "recommendation-subsection";

                stepsSection.Controls.Add(
                    new LiteralControl(
                        "<strong>Implementation Steps</strong>"));

                stepsSection.Controls.Add(
                    new LiteralControl(
                        "<ol class='recommendation-list'>"));

                if (recommendation.ImplementationSteps != null &&
                    recommendation.ImplementationSteps.Count > 0)
                {
                    foreach (var step
                        in recommendation.ImplementationSteps)
                    {
                        stepsSection.Controls.Add(
                            new LiteralControl(
                                "<li>" +
                                Server.HtmlEncode(step) +
                                "</li>"));
                    }
                }
                else
                {
                    stepsSection.Controls.Add(
                        new LiteralControl(
                            "<li>No implementation steps specified.</li>"));
                }

                stepsSection.Controls.Add(
                    new LiteralControl(
                        "</ol>"));

                card.Controls.Add(stepsSection);


                // -------------------------------------------------
                // Metadata
                // -------------------------------------------------

                var meta =
                    new Panel();

                meta.CssClass =
                    "recommendation-meta";

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


                // -------------------------------------------------
                // Add card to page
                // -------------------------------------------------

                phRecommendations.Controls.Add(card);
            }
        }
    }
}