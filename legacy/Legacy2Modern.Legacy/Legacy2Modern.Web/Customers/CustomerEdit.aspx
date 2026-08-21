<%@ Page Title="Customer"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="CustomerEdit.aspx.cs"
    Inherits="Legacy2Modern.Web.Customers.CustomerEdit" %>

<asp:Content
    ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="container">

        <h2>
            <asp:Label
                ID="lblPageTitle"
                runat="server"
                Text="Customer" />
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
                    Customer Code *
                </label>

                <div class="col-md-6">
                    <asp:TextBox
                        ID="txtCustomerCode"
                        runat="server"
                        CssClass="form-control" />

                    <asp:RequiredFieldValidator
                        ID="valCustomerCode"
                        runat="server"
                        ControlToValidate="txtCustomerCode"
                        ErrorMessage="Customer code is required."
                        CssClass="text-danger" />
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">
                    First Name *
                </label>

                <div class="col-md-6">
                    <asp:TextBox
                        ID="txtFirstName"
                        runat="server"
                        CssClass="form-control" />

                    <asp:RequiredFieldValidator
                        ID="valFirstName"
                        runat="server"
                        ControlToValidate="txtFirstName"
                        ErrorMessage="First name is required."
                        CssClass="text-danger" />
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">
                    Last Name
                </label>

                <div class="col-md-6">
                    <asp:TextBox
                        ID="txtLastName"
                        runat="server"
                        CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">
                    Email
                </label>

                <div class="col-md-6">
                    <asp:TextBox
                        ID="txtEmail"
                        runat="server"
                        CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">
                    Phone
                </label>

                <div class="col-md-6">
                    <asp:TextBox
                        ID="txtPhone"
                        runat="server"
                        CssClass="form-control" />
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
                            Text="Inactive"
                            Value="Inactive" />

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
            NavigateUrl="~/Customers/CustomerList.aspx"
            runat="server"
            CssClass="btn btn-default">
            Cancel
        </asp:HyperLink>

    </div>

</asp:Content>