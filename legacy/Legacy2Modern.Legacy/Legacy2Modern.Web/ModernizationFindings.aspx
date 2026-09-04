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
        <div class="row mb-4">

            <div class="col-md-3">
                <div class="card">
                    <div class="card-body">
                        <h6>Total Findings</h6>
                        <h3>
                            <asp:Label
                                ID="lblTotalFindings"
                                runat="server" />
                        </h3>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card">
                    <div class="card-body">
                        <h6>High/Critical Risk</h6>
                        <h3>
                            <asp:Label
                                ID="lblHighRisk"
                                runat="server" />
                        </h3>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card">
                    <div class="card-body">
                        <h6>High/Critical Priority</h6>
                        <h3>
                            <asp:Label
                                ID="lblHighPriority"
                                runat="server" />
                        </h3>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card">
                    <div class="card-body">
                        <h6>Identified</h6>
                        <h3>
                            <asp:Label
                                ID="lblIdentified"
                                runat="server" />
                        </h3>
                    </div>
                </div>
            </div>

        </div>

        <h5>Estimated Effort Distribution</h5>

        <div class="mb-4">

            <asp:Label
                ID="lblEffortDistribution"
                runat="server" />

        </div>
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
