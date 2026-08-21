<%@ Page Title="Customer Details"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="CustomerDetails.aspx.cs"
    Inherits="Legacy2Modern.Web.Customers.CustomerDetails" %>

<asp:Content
    ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="container">

        <h2>Customer Details</h2>

        <hr />

        <asp:Label
            ID="lblMessage"
            runat="server"
            CssClass="text-danger" />

        <div class="form-horizontal">

            <div class="form-group">
                <label class="control-label col-md-2">
                    Customer Code
                </label>

                <div class="col-md-6">
                    <asp:Label
                        ID="lblCustomerCode"
                        runat="server"
                        CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">
                    First Name
                </label>

                <div class="col-md-6">
                    <asp:Label
                        ID="lblFirstName"
                        runat="server"
                        CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">
                    Last Name
                </label>

                <div class="col-md-6">
                    <asp:Label
                        ID="lblLastName"
                        runat="server"
                        CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">
                    Email
                </label>

                <div class="col-md-6">
                    <asp:Label
                        ID="lblEmail"
                        runat="server"
                        CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">
                    Phone
                </label>

                <div class="col-md-6">
                    <asp:Label
                        ID="lblPhone"
                        runat="server"
                        CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">
                    Status
                </label>

                <div class="col-md-6">
                    <asp:Label
                        ID="lblStatus"
                        runat="server"
                        CssClass="form-control" />
                </div>
            </div>

        </div>

        <br />

        <asp:HyperLink
            ID="lnkEdit"
            runat="server"
            CssClass="btn btn-warning">
            Edit Customer
        </asp:HyperLink>

        &nbsp;

        <asp:HyperLink
            NavigateUrl="~/Customers/CustomerList.aspx"
            runat="server"
            CssClass="btn btn-default">
            Back
        </asp:HyperLink>

    </div>

</asp:Content>