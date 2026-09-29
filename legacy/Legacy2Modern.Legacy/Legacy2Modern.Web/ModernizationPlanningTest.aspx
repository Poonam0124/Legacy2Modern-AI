<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="ModernizationPlanningTest.aspx.cs"
    Inherits="Legacy2Modern.Web.ModernizationPlanningTest" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Modernization Planning Test</title>
</head>

<body>
    <form id="form1" runat="server">

        <h1>Modernization Planning Test</h1>

        <asp:Button
            ID="btnGeneratePlan"
            runat="server"
            Text="Generate Modernization Plan"
            OnClick="btnGeneratePlan_Click" />

        <hr />

        <asp:Label
            ID="lblResult"
            runat="server" />

    </form>
</body>
</html>