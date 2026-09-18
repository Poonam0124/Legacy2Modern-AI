<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="ModernizationAIAnalysis.aspx.cs"
    Inherits="Legacy2Modern.Web.ModernizationAIAnalysis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>AI Modernization Analysis</title>

    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
            margin: 0;
            background: #f4f6f8;
            color: #222;
        }

        .page-container {
            max-width: 1200px;
            margin: 40px auto;
            padding: 0 20px;
        }

        .page-header {
            margin-bottom: 25px;
        }

            .page-header h1 {
                margin-bottom: 8px;
            }

            .page-header p {
                color: #666;
                margin-top: 0;
            }

        .action-bar {
            background: #ffffff;
            padding: 18px;
            border-radius: 8px;
            margin-bottom: 25px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.08);
        }

        .btn-primary {
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
        }

        .analysis-section {
            background: #ffffff;
            padding: 25px;
            border-radius: 8px;
            margin-bottom: 20px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.08);
        }

            .analysis-section h2 {
                margin-top: 0;
                margin-bottom: 15px;
                font-size: 20px;
            }

        .analysis-content {
            line-height: 1.6;
            white-space: pre-line;
        }

        .section-description {
            color: #666;
            margin-bottom: 15px;
        }

        .status-message {
            margin-top: 15px;
            color: #555;
        }

        .recommendations-section {
            margin-top: 25px;
        }

        .recommendation-card {
            background: #ffffff;
            border-radius: 8px;
            padding: 22px;
            margin-bottom: 18px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.08);
            border-left: 4px solid #555;
        }

        .recommendation-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 18px;
        }

            .recommendation-header h3 {
                margin: 0;
                font-size: 18px;
            }

        .finding-id {
            font-weight: bold;
        }

        .recommendation-item {
            margin-bottom: 16px;
        }

            .recommendation-item strong {
                display: block;
                margin-bottom: 6px;
            }

        .recommendation-content {
            line-height: 1.6;
            white-space: pre-line;
        }

        .recommendation-meta {
            display: flex;
            gap: 25px;
            margin-top: 18px;
            padding-top: 15px;
            border-top: 1px solid #eee;
            flex-wrap: wrap;
        }

        .meta-item {
            font-size: 14px;
        }

        .meta-label {
            font-weight: bold;
        }
                .recommendation-list {
            margin: 8px 0 0 0;
            padding-left: 20px;
            line-height: 1.6;
        }

        .recommendation-list li {
            margin-bottom: 6px;
        }

        .recommendation-subsection {
            margin-top: 18px;
        }

        .recommendation-subsection strong {
            display: block;
            margin-bottom: 8px;
        }
    </style>

</head>

<body>

    <form id="form1" runat="server">

        <div class="page-container">

            <div class="page-header">

                <h1>AI Modernization Analysis</h1>

                <p>
                    AI-assisted analysis of legacy application modernization opportunities.
                </p>

            </div>

            <div class="action-bar">

                <asp:Button
                    ID="btnRunAnalysis"
                    runat="server"
                    Text="Run AI Analysis"
                    CssClass="btn-primary"
                    OnClick="btnRunAnalysis_Click" />

                <asp:Label
                    ID="lblRecommendations"
                    runat="server"
                    CssClass="status-message" />

            </div>


            <div class="analysis-section">

                <h2>Overall Assessment</h2>

                <p class="section-description">
                    High-level assessment generated from the identified modernization findings.
                </p>

                <div class="analysis-content">

                    <asp:Label
                        ID="lblOverallAssessment"
                        runat="server" />

                </div>

            </div>


            <div class="analysis-section">

                <h2>Recommended Modernization Approach</h2>

                <p class="section-description">
                    Suggested strategy for addressing the identified technical debt incrementally.
                </p>

                <div class="analysis-content">

                    <asp:Label
                        ID="lblRecommendedApproach"
                        runat="server" />

                </div>

            </div>


            <div class="analysis-section">

                <h2>Target Architecture</h2>

                <p class="section-description">
                    Proposed architectural direction based on the current legacy application.
                </p>

                <div class="analysis-content">

                    <asp:Label
                        ID="lblTargetArchitecture"
                        runat="server" />

                </div>

            </div>
            <div class="recommendations-section">

                <h2>AI Modernization Recommendations</h2>

                <p class="section-description">
                    Detailed recommendations generated for the identified modernization findings.
                </p>

                <asp:PlaceHolder
                    ID="phRecommendations"
                    runat="server" />

            </div>

        </div>

    </form>

</body>

</html>
