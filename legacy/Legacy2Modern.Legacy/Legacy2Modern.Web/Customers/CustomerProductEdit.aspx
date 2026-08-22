<%@ Page Title="Customer Product"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="CustomerProductEdit.aspx.cs"
    Inherits="Legacy2Modern.Web.Customers.CustomerProductEdit" %>

<asp:Content
    ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="container">

        <h2>
            <asp:Label
                ID="lblPageTitle"
                runat="server"
                Text="Customer Product" />
        </h2>

        <hr />

        <asp:ValidationSummary
            ID="ValidationSummary1"
            runat="server"
            CssClass="text-danger" />

        <asp:Label
            ID="lblMessage"
            runat="server"
            CssClass="text-danger" />

        <div class="form-horizontal">

            <div class="form-group">

                <label class="control-label col-md-2">
                    Product *
                </label>

                <div class="col-md-6">

                    <asp:DropDownList
                        ID="ddlProduct"
                        runat="server"
                        CssClass="form-control"
                        DataTextField="ProductName"
                        DataValueField="ProductId">
                    </asp:DropDownList>

                </div>

            </div>

            <div class="form-group">

                <label class="control-label col-md-2">
                    Subscription Number
                </label>

                <div class="col-md-6">

                    <asp:TextBox
                        ID="txtSubscriptionNumber"
                        runat="server"
                        CssClass="form-control" />

                </div>

            </div>

            <div class="form-group">

                <label class="control-label col-md-2">
                    Start Date
                </label>

                <div class="col-md-6">

                    <asp:TextBox
                        ID="txtStartDate"
                        runat="server"
                        CssClass="form-control"
                        TextMode="Date" />

                </div>

            </div>

            <div class="form-group">

                <label class="control-label col-md-2">
                    End Date
                </label>

                <div class="col-md-6">

                    <asp:TextBox
                        ID="txtEndDate"
                        runat="server"
                        CssClass="form-control"
                        TextMode="Date" />

                </div>

            </div>

            <div class="form-group">

                <label class="control-label col-md-2">
                    Status
                </label>

                <div class="col-md-6">

                    <asp:DropDownList
                        ID="ddlStatus"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem
                            Text="Active"
                            Value="Active" />

                        <asp:ListItem
                            Text="Suspended"
                            Value="Suspended" />

                        <asp:ListItem
                            Text="Expired"
                            Value="Expired" />

                    </asp:DropDownList>

                </div>

            </div>

        </div>

        <br />

        <asp:Button
            ID="btnSave"
            runat="server"
            Text="Save"
            CssClass="btn btn-primary"
            OnClick="btnSave_Click" />

        &nbsp;

        <asp:HyperLink
            ID="lnkCancel"
            runat="server"
            CssClass="btn btn-default"
            Text="Cancel" />

    </div>

</asp:Content>