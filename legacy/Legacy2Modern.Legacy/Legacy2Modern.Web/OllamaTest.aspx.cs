using System;
using System.Collections.Generic;
using System.Text;
using Legacy2Modern.Business.Models.AI;
using Legacy2Modern.Business.Models;
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

                var findings =
                    findingService.GetExportData();

                var context =
                    new ModernizationAnalysisContext
                    {
                        ApplicationName =
                            "Legacy2Modern-AI",

                        ApplicationDescription =
                            "A legacy ASP.NET Web Forms application being incrementally modernized.",

                        TechnologyStack =
                            "ASP.NET Web Forms, .NET Framework 4.8, C#, Entity Framework 6, SQL Server",

                        ModernizationGoal =
                            "Identify practical modernization opportunities while minimizing business disruption.",

                        Findings = findings
                    };

                var promptBuilder =
                    new ModernizationPromptBuilder();

                var requestBuilder =
                    new ModernizationAIRequestBuilder(
                        promptBuilder);

                var request =
                    requestBuilder.Build(context);

                var provider =
                    new OllamaAIProvider(
                        "http://localhost:11434/api/generate",
                        "qwen3:4b");

                var aiService =
                    new ModernizationAIService(
                        provider);

                var response =
                    aiService.Analyze(request);

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
                    "<strong>Error:</strong> " +
                    Server.HtmlEncode(
                        ex.Message);
            }
        }
    }
}