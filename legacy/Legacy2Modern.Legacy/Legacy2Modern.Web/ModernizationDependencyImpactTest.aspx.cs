using System;
using System.Configuration;
using System.Text;
using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Services;
using Legacy2Modern.Business.Services.AI;

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
                lblResult.Text = "Generating analysis...";

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
                lblResult.Text =
                    "<pre>" +
                    Server.HtmlEncode(ex.ToString()) +
                    "</pre>";
            }
        }

        private void DisplayAnalysis(
            ModernizationDependencyImpactAnalysis analysis)
        {
            if (analysis == null)
            {
                lblResult.Text =
                    "No dependency and impact analysis was returned.";

                return;
            }

            var output = new StringBuilder();

            output.AppendLine(
                "<h2>" +
                Server.HtmlEncode(analysis.AnalysisTitle) +
                "</h2>");

            output.AppendLine(
                "<p><strong>Provider:</strong> Ollama (configured provider)</p>");

            output.AppendLine(
                "<p><strong>Overall Assessment:</strong><br />" +
                Server.HtmlEncode(analysis.OverallAssessment) +
                "</p>");

            output.AppendLine("<h3>Dependencies</h3>");

            if (analysis.Dependencies != null)
            {
                foreach (var dependency in analysis.Dependencies)
                {
                    output.AppendLine("<div>");

                    output.AppendLine(
                        "<h4>" +
                        Server.HtmlEncode(dependency.DependencyId) +
                        " - " +
                        Server.HtmlEncode(dependency.DependencyType) +
                        "</h4>");

                    output.AppendLine(
                        "<p><strong>Source:</strong> " +
                        Server.HtmlEncode(dependency.SourceId) +
                        "</p>");

                    output.AppendLine(
                        "<p><strong>Target:</strong> " +
                        Server.HtmlEncode(dependency.TargetId) +
                        "</p>");

                    output.AppendLine(
                        "<p><strong>Description:</strong> " +
                        Server.HtmlEncode(dependency.Description) +
                        "</p>");

                    output.AppendLine(
                        "<p><strong>Impact:</strong> " +
                        Server.HtmlEncode(dependency.Impact) +
                        "</p>");

                    output.AppendLine("</div>");
                }
            }

            output.AppendLine("<h3>Impacts</h3>");

            if (analysis.Impacts != null)
            {
                foreach (var impact in analysis.Impacts)
                {
                    output.AppendLine("<div>");

                    output.AppendLine(
                        "<h4>" +
                        Server.HtmlEncode(impact.ImpactId) +
                        " - " +
                        Server.HtmlEncode(impact.ImpactArea) +
                        "</h4>");

                    output.AppendLine(
                        "<p><strong>Finding:</strong> " +
                        Server.HtmlEncode(impact.FindingId) +
                        "</p>");

                    output.AppendLine(
                        "<p><strong>Level:</strong> " +
                        Server.HtmlEncode(impact.ImpactLevel) +
                        "</p>");

                    output.AppendLine(
                        "<p><strong>Description:</strong> " +
                        Server.HtmlEncode(impact.Description) +
                        "</p>");

                    output.AppendLine("</div>");
                }
            }

            lblResult.Text = output.ToString();
        }
    }
}