<%@ Page Title="Modernization Findings"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="ModernizationFindings.aspx.cs"
    Inherits="Legacy2Modern.Web.ModernizationFindings" %>

<asp:Content ID="MainContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="container mt-4">

        <h2>Modernization Findings</h2>

        <p class="text-muted">
            Findings identified during legacy application assessment.
        </p>

        <asp:GridView ID="gvFindings"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped"
            EmptyDataText="No modernization findings found.">

            <Columns>

                <asp:BoundField
                    DataField="Id"
                    HeaderText="ID" />

                <asp:BoundField
                    DataField="Category"
                    HeaderText="Category" />

                <asp:BoundField
                    DataField="Title"
                    HeaderText="Finding" />

                <asp:BoundField
                    DataField="Description"
                    HeaderText="Description" />

                <asp:BoundField
                    DataField="Risk"
                    HeaderText="Risk" />

                <asp:BoundField
                    DataField="Priority"
                    HeaderText="Priority" />

                <asp:BoundField
                    DataField="AffectedLayer"
                    HeaderText="Affected Layer" />

                <asp:BoundField
                    DataField="ModernizationType"
                    HeaderText="Modernization Type" />

                <asp:BoundField
                    DataField="Status"
                    HeaderText="Status" />

                <asp:BoundField
                    DataField="EstimatedEffort"
                    HeaderText="Estimated Effort" />

                <asp:BoundField
                    DataField="Recommendation"
                    HeaderText="Recommendation" />

            </Columns>

        </asp:GridView>

    </div>

</asp:Content>
