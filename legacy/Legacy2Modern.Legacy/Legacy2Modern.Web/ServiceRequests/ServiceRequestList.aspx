<%@ Page Title="Service Requests"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="ServiceRequestList.aspx.cs"
    Inherits="Legacy2Modern.Web.ServiceRequests.ServiceRequestList" %>

<asp:Content
    ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="container">

        <h2>Service Requests</h2>

        <hr />

        <div class="row">

            <div class="col-md-4">

                <asp:TextBox
                    ID="txtSearch"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Request number, subject or customer" />

            </div>

            <div class="col-md-2">

                <asp:DropDownList
                    ID="ddlStatus"
                    runat="server"
                    CssClass="form-control">

                    <asp:ListItem
                        Text="All Statuses"
                        Value="All" />

                    <asp:ListItem
                        Text="Open"
                        Value="Open" />

                    <asp:ListItem
                        Text="Assigned"
                        Value="Assigned" />

                    <asp:ListItem
                        Text="In Progress"
                        Value="In Progress" />

                    <asp:ListItem
                        Text="Resolved"
                        Value="Resolved" />

                    <asp:ListItem
                        Text="Closed"
                        Value="Closed" />

                </asp:DropDownList>

            </div>

            <div class="col-md-2">

                <asp:DropDownList
                    ID="ddlPriority"
                    runat="server"
                    CssClass="form-control">

                    <asp:ListItem
                        Text="All Priorities"
                        Value="All" />

                    <asp:ListItem
                        Text="High"
                        Value="High" />

                    <asp:ListItem
                        Text="Medium"
                        Value="Medium" />

                    <asp:ListItem
                        Text="Low"
                        Value="Low" />

                </asp:DropDownList>

            </div>

            <div class="col-md-2">

                <asp:Button
                    ID="btnSearch"
                    runat="server"
                    Text="Search"
                    CssClass="btn btn-primary"
                    OnClick="btnSearch_Click" />

            </div>

        </div>

        <br />

        <asp:Label
            ID="lblMessage"
            runat="server"
            CssClass="text-danger" />

        <asp:GridView
            ID="gvServiceRequests"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped">

            <Columns>

                <asp:BoundField
                    DataField="RequestNumber"
                    HeaderText="Request #" />

                <asp:BoundField
                    DataField="Subject"
                    HeaderText="Subject" />

                <asp:BoundField
                    DataField="RequestType"
                    HeaderText="Type" />

                <asp:BoundField
                    DataField="Priority"
                    HeaderText="Priority" />

                <asp:BoundField
                    DataField="Status"
                    HeaderText="Status" />

                <asp:BoundField
                    DataField="CreatedDate"
                    HeaderText="Created"
                    DataFormatString="{0:dd-MMM-yyyy HH:mm}" />

                <asp:HyperLinkField
                    Text="View"
                    HeaderText="Action"
                    DataNavigateUrlFields="ServiceRequestId"
                    DataNavigateUrlFormatString="ServiceRequestDetails.aspx?id={0}"
                    ControlStyle-CssClass="btn btn-sm btn-info" />

            </Columns>

        </asp:GridView>

    </div>

</asp:Content>