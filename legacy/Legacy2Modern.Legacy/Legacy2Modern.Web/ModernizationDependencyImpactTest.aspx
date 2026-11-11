
<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="ModernizationDependencyImpactTest.aspx.cs"
    Inherits="Legacy2Modern.Web.ModernizationDependencyImpactTest" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Dependency &amp; Impact Analysis</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <style>
        body {
            margin: 0;
            padding: 32px;
            background: #f4f6f9;
            color: #243247;
            font-family: "Segoe UI", Arial, sans-serif;
        }

        .container {
            max-width: 1100px;
            margin: 0 auto;
        }

        .page-header {
            margin-bottom: 24px;
        }

        .page-header h1 {
            margin: 0 0 8px;
            font-size: 28px;
        }

        .subtitle {
            margin: 0;
            color: #64748b;
            line-height: 1.6;
        }

        .toolbar {
            display: flex;
            align-items: center;
            gap: 16px;
            flex-wrap: wrap;
            margin-bottom: 24px;
            padding: 20px;
            background: #ffffff;
            border: 1px solid #e2e8f0;
            border-radius: 10px;
        }

        .primary-button {
            padding: 12px 20px;
            border: 0;
            border-radius: 6px;
            background: #2457c5;
            color: #ffffff;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
        }

        .primary-button:hover {
            background: #1946a5;
        }

        .section {
            margin-bottom: 24px;
            padding: 24px;
            background: #ffffff;
            border: 1px solid #e2e8f0;
            border-radius: 10px;
        }

        .section h2 {
            margin: 0 0 16px;
            font-size: 20px;
        }

        .section-description {
            margin: -8px 0 18px;
            color: #64748b;
            line-height: 1.5;
        }

        .result-area {
            line-height: 1.7;
            overflow-wrap: anywhere;
        }

        .placeholder {
            color: #64748b;
        }

        .status {
            display: block;
            margin-top: 12px;
            line-height: 1.5;
        }

        @media (max-width: 600px) {
            body {
                padding: 16px;
            }

            .section {
                padding: 18px;
            }

            .page-header h1 {
                font-size: 23px;
            }

            .primary-button {
                width: 100%;
            }
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">

            <header class="page-header">
                <h1>Dependency &amp; Impact Analysis</h1>
                <p class="subtitle">
                    Explore dependencies between modernization activities
                    and identify potential risks and impacts.
                </p>
            </header>

            <div class="toolbar">
                <asp:Button
                    ID="btnAnalyze"
                    runat="server"
                    Text="Generate Analysis"
                    CssClass="primary-button"
                    OnClick="btnAnalyze_Click" />

                <asp:Label
                    ID="lblStatus"
                    runat="server"
                    CssClass="status" />
            </div>

            <section class="section">
                <h2>Overall Assessment</h2>
                <p class="section-description">
                    Summary of the modernization dependency and impact analysis.
                </p>

                <div class="result-area">
                    <asp:Label
                        ID="lblOverallAssessment"
                        runat="server"
                        CssClass="placeholder"
                        Text="Generate an analysis to view the overall assessment." />
                </div>
            </section>

            <section class="section">
                <h2>Dependencies</h2>
                <p class="section-description">
                    Prerequisite relationships between modernization plan items.
                </p>

                <div class="result-area">
                    <asp:Label
                        ID="lblDependencies"
                        runat="server"
                        CssClass="placeholder"
                        Text="Dependency details will appear here." />
                </div>
            </section>

            <section class="section">
                <h2>Impacts</h2>
                <p class="section-description">
                    Potential effects on application behavior, architecture,
                    reliability, and delivery.
                </p>

                <div class="result-area">
                    <asp:Label
                        ID="lblImpacts"
                        runat="server"
                        CssClass="placeholder"
                        Text="Impact details will appear here." />
                </div>
            </section>

            <section class="section">
                <h2>Analysis Details</h2>

                <div class="result-area">
                    <asp:Label
                        ID="lblResult"
                        runat="server" />
                </div>
            </section>

        </div>
    </form>
</body>
</html>
