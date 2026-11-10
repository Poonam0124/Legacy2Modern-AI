<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="ModernizationDependencyImpactTest.aspx.cs"
    Inherits="Legacy2Modern.Web.ModernizationDependencyImpactTest" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Dependency &amp; Impact Analysis Test</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Dependency &amp; Impact Analysis Test</h1>

        <asp:Button
            ID="btnAnalyze"
            runat="server"
            Text="Generate Dependency &amp; Impact Analysis"
            OnClick="btnAnalyze_Click" />

        <hr />

        <asp:Label
            ID="lblResult"
            runat="server" />
    </form>
</body>
</html>