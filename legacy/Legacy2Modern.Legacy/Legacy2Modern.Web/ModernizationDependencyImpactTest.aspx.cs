using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services;
using Legacy2Modern.Business.Services.AI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Legacy2Modern.Web
{
    public partial class ModernizationDependencyImpactTest
        : System.Web.UI.Page
    {
        protected void btnAnalyze_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                lblStatus.Text = "Generating analysis. Please wait...";
                lblStatus.ForeColor = System.Drawing.Color.DarkSlateGray;

                lblOverallAssessment.Text = string.Empty;
                lblDependencies.Text = string.Empty;
                lblImpacts.Text = string.Empty;
                lblResult.Text = string.Empty;

                // 1. Retrieve existing modernization findings.
                var findingService =
                    new ModernizationFindingService();

                var findings =
                    findingService.GetExportData();

                if (findings == null || findings.Count == 0)
                {
                    throw new InvalidOperationException(
                        "No modernization findings are available.");
                }

                // 2. Configure the existing M5 AI provider.
                var providerConfiguration =
                    new AIProviderConfiguration
                    {
                        ProviderName =
                            ConfigurationManager.AppSettings["AIProvider"],

                        ModelName =
                            ConfigurationManager.AppSettings["AIModel"],

                        Endpoint =
                            ConfigurationManager.AppSettings["AIEndpoint"],

                        TimeoutSeconds =
                            int.Parse(
                                ConfigurationManager.AppSettings[
                                    "AITimeoutSeconds"])
                    };
                System.Diagnostics.Debug.WriteLine(
                 "AI Endpoint: " + providerConfiguration.Endpoint);

                System.Diagnostics.Debug.WriteLine(
                    "AI Model: " + providerConfiguration.ModelName);

                System.Diagnostics.Debug.WriteLine(
                    "AI Timeout Seconds: " +
                    providerConfiguration.TimeoutSeconds);

                // 3. Generate M5 recommendations.
                var promptBuilder =
                    new ModernizationPromptBuilder();

                var requestBuilder =
                    new ModernizationAIRequestBuilder(
                        promptBuilder);

                var providerFactory =
                    new AIProviderFactory();

                var primaryProvider =
                    providerFactory.Create(
                        providerConfiguration);

                var fallbackProvider =
                    new MockModernizationAIService();

                var fallbackConfiguration =
                    new AIFallbackConfiguration
                    {
                        Enabled =
                            bool.Parse(
                                ConfigurationManager.AppSettings[
                                    "AIFallbackEnabled"]),

                        ProviderName =
                            ConfigurationManager.AppSettings[
                                "AIFallbackProvider"]
                    };

                var orchestrator =
                    new AIProviderOrchestrator(
                        primaryProvider,
                        fallbackProvider,
                        fallbackConfiguration);

                var aiService =
                    new ModernizationAIService(
                        orchestrator);

                var analysisService =
                    new ModernizationAnalysisService(
                        findingService,
                        promptBuilder,
                        requestBuilder,
                        aiService);

                var analysis =
                    analysisService.Analyze();

                if (analysis == null)
                {
                    throw new InvalidOperationException(
                        "M5 returned a null analysis response.");
                }

                if (analysis.Recommendations == null)
                {
                    throw new InvalidOperationException(
                        "M5 returned an analysis, but Recommendations is null.");
                }

                if (analysis.Recommendations.Count == 0)
                {
                    throw new InvalidOperationException(
                        "M5 returned zero recommendations. " +
                        "Check whether the AI provider response was empty " +
                        "or whether parsing or validation discarded the recommendations.");
                }


                // 4. Generate the M6 modernization plan.
                var planningContext =
                    new ModernizationPlanningContext
                    {
                        ApplicationName = "Legacy2Modern-AI",

                        ApplicationDescription =
                            "A legacy ASP.NET Web Forms application being incrementally modernized.",

                        TechnologyStack =
                            "ASP.NET Web Forms, .NET Framework 4.8, C#, Entity Framework 6, SQL Server",

                        ModernizationGoal =
                            "Identify practical modernization opportunities while minimizing business disruption.",

                        Findings = findings,

                        Recommendations =
                            analysis.Recommendations
                    };

                var planningPromptBuilder =
                    new ModernizationPlanningPromptBuilder();

                var planningRequestBuilder =
                    new ModernizationPlanningRequestBuilder(
                        planningPromptBuilder);

                var planningResponseParser =
                    new ModernizationPlanningResponseParser();

                var planningResponseValidator =
                    new ModernizationPlanningResponseValidator();

                var planningProviderFactory =
                    new AIPlanningProviderFactory(
                        planningResponseParser,
                        planningResponseValidator);

                var planningProvider =
                    planningProviderFactory.Create(
                        providerConfiguration);

                var planningService =
                    new ModernizationPlanningService(
                        planningRequestBuilder,
                        planningProvider);

                var planningResponse =
                    planningService.CreatePlan(
                        planningContext);

                if (planningResponse == null ||
                    planningResponse.Plan == null)
                {
                    throw new InvalidOperationException(
                        "No modernization plan was generated.");
                }

                // 5. Build the M7 context using the actual M6 plan.
                var dependencyImpactContext =
                    new ModernizationDependencyImpactContext
                    {
                        ApplicationName =
                            planningContext.ApplicationName,

                        ApplicationDescription =
                            planningContext.ApplicationDescription,

                        TechnologyStack =
                            planningContext.TechnologyStack,

                        ModernizationGoal =
                            planningContext.ModernizationGoal,

                        Findings = findings,

                        Recommendations =
                            analysis.Recommendations,

                        Plan = planningResponse.Plan
                    };

                // 6. Create the M7 parser, validator and provider.
                var dependencyImpactParser =
                    new ModernizationDependencyImpactResponseParser();

                var dependencyImpactValidator =
                    new ModernizationDependencyImpactResponseValidator();

                var dependencyImpactProviderFactory =
                    new AIDependencyImpactProviderFactory(
                        providerConfiguration,
                        dependencyImpactParser,
                        dependencyImpactValidator);

                var dependencyImpactProvider =
                    dependencyImpactProviderFactory.Create();

                var dependencyImpactRequestBuilder =
                    new ModernizationDependencyImpactRequestBuilder(
                        new ModernizationDependencyImpactPromptBuilder());

                var dependencyImpactService =
                     new ModernizationDependencyImpactAnalysisService(
                         dependencyImpactRequestBuilder,
                         dependencyImpactProvider,
                         dependencyImpactValidator);

                // 7. Execute the dependency and impact analysis.
                var result =
                    dependencyImpactService.Analyze(
                        dependencyImpactContext);

                DisplayAnalysis(result);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Analysis failed.";
                lblStatus.ForeColor = System.Drawing.Color.DarkRed;

                lblResult.Text = "<pre>" +
                    Server.HtmlEncode(ex.ToString()) +
                    "</pre>";
            }
        }


        private void DisplayAnalysis(
            ModernizationDependencyImpactAnalysis analysis)
        {
            if (analysis == null)
            {
                throw new InvalidOperationException(
                    "No dependency and impact analysis was returned.");
            }

            // 1. Display the overall assessment.
            lblStatus.Text = "Analysis completed successfully.";
            lblStatus.ForeColor = System.Drawing.Color.DarkGreen;

            lblOverallAssessment.Text =
                Server.HtmlEncode(
                    analysis.OverallAssessment ?? string.Empty);

            // 2. Display dependencies.
            var dependencyHtml = new StringBuilder();

            if (analysis.Dependencies == null ||
                analysis.Dependencies.Count == 0)
            {
                dependencyHtml.Append(
                    "<p>No dependencies were identified.</p>");
            }
            else
            {
                foreach (var dependency in analysis.Dependencies)
                {
                    dependencyHtml.Append("<div class='result-card'>");

                    dependencyHtml.Append("<h3>");
                    dependencyHtml.Append(
                        Server.HtmlEncode(dependency.DependencyId ?? ""));
                    dependencyHtml.Append(" - ");
                    dependencyHtml.Append(
                        Server.HtmlEncode(dependency.DependencyType ?? ""));
                    dependencyHtml.Append("</h3>");

                    dependencyHtml.Append("<p><strong>Source:</strong> ");
                    dependencyHtml.Append(
                        Server.HtmlEncode(dependency.SourceId ?? ""));
                    dependencyHtml.Append("</p>");

                    dependencyHtml.Append("<p><strong>Target:</strong> ");
                    dependencyHtml.Append(
                        Server.HtmlEncode(dependency.TargetId ?? ""));
                    dependencyHtml.Append("</p>");

                    dependencyHtml.Append("<p><strong>Description:</strong> ");
                    dependencyHtml.Append(
                        Server.HtmlEncode(dependency.Description ?? ""));
                    dependencyHtml.Append("</p>");

                    dependencyHtml.Append("<p><strong>Impact:</strong> ");
                    dependencyHtml.Append(
                        Server.HtmlEncode(dependency.Impact ?? ""));
                    dependencyHtml.Append("</p>");

                    AppendAnalysisList(
                        dependencyHtml,
                        "Affected Components",
                        dependency.AffectedComponents);

                    AppendAnalysisList(
                        dependencyHtml,
                        "Reasons",
                        dependency.Reasons);

                    dependencyHtml.Append("</div>");
                }
            }

            lblDependencies.Text = dependencyHtml.ToString();

            // 3. Display impacts.
            var impactHtml = new StringBuilder();

            if (analysis.Impacts == null ||
                analysis.Impacts.Count == 0)
            {
                impactHtml.Append(
                    "<p>No impacts were identified.</p>");
            }
            else
            {
                foreach (var impact in analysis.Impacts)
                {
                    impactHtml.Append("<div class='result-card'>");

                    impactHtml.Append("<h3>");
                    impactHtml.Append(
                        Server.HtmlEncode(impact.ImpactId ?? ""));
                    impactHtml.Append(" - ");
                    impactHtml.Append(
                        Server.HtmlEncode(impact.ImpactArea ?? ""));
                    impactHtml.Append("</h3>");

                    impactHtml.Append("<p><strong>Finding:</strong> ");
                    impactHtml.Append(
                        Server.HtmlEncode(impact.FindingId ?? ""));
                    impactHtml.Append("</p>");

                    impactHtml.Append("<p><strong>Impact Level:</strong> ");
                    impactHtml.Append(
                        Server.HtmlEncode(impact.ImpactLevel ?? ""));
                    impactHtml.Append("</p>");

                    impactHtml.Append("<p><strong>Description:</strong> ");
                    impactHtml.Append(
                        Server.HtmlEncode(impact.Description ?? ""));
                    impactHtml.Append("</p>");

                    AppendAnalysisList(
                        impactHtml,
                        "Affected Components",
                        impact.AffectedComponents);

                    AppendAnalysisList(
                        impactHtml,
                        "Risks",
                        impact.Risks);

                    AppendAnalysisList(
                        impactHtml,
                        "Mitigations",
                        impact.Mitigations);

                    impactHtml.Append("</div>");
                }
            }

            lblImpacts.Text = impactHtml.ToString();

            // 4. Keep the analysis title and provider in Analysis Details.
            var detailsHtml = new StringBuilder();

            detailsHtml.Append("<p><strong>Analysis Title:</strong> ");
            detailsHtml.Append(
                Server.HtmlEncode(analysis.AnalysisTitle ?? ""));
            detailsHtml.Append("</p>");

            detailsHtml.Append(
                "<p><strong>Provider:</strong> Ollama (configured provider)</p>");

            lblResult.Text = detailsHtml.ToString();
        }

        private void AppendAnalysisList(
            StringBuilder html,
            string heading,
            IEnumerable<string> items)
        {
            html.Append("<p><strong>");
            html.Append(Server.HtmlEncode(heading));
            html.Append("</strong></p>");

            if (items == null)
            {
                html.Append("<p>None specified.</p>");
                return;
            }

            var listHtml = new StringBuilder();
            var hasItems = false;

            foreach (var item in items)
            {
                if (!hasItems)
                {
                    listHtml.Append("<ul>");
                    hasItems = true;
                }

                listHtml.Append("<li>");
                listHtml.Append(
                    Server.HtmlEncode(item ?? string.Empty));
                listHtml.Append("</li>");
            }

            if (hasItems)
            {
                listHtml.Append("</ul>");
                html.Append(listHtml.ToString());
            }
            else
            {
                html.Append("<p>None specified.</p>");
            }
        }

    }
}