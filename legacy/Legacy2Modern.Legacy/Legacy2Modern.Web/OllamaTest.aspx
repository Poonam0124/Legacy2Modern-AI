<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="OllamaTest.aspx.cs"
    Inherits="Legacy2Modern.Web.OllamaTest" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Ollama AI Test</title>
</head>

<body>
    <form id="form1" runat="server">

        <h2>Ollama AI Integration Test</h2>

        <asp:Button
            ID="btnAnalyze"
            runat="server"
            Text="Analyze Legacy Application"
            OnClick="btnAnalyze_Click" />

        <br />
        <br />

        <asp:Label
            ID="lblResult"
            runat="server"
            EnableViewState="false" />

    </form>
</body>
</html>