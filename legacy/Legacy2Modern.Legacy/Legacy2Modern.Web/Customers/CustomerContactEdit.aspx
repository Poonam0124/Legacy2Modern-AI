<%@ Page Title="Customer Contact"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="CustomerContactEdit.aspx.cs"
    Inherits="Legacy2Modern.Web.Customers.CustomerContactEdit" %>

<asp:Content
    ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="container">

        <h2>
            <asp:Label
                ID="lblPageTitle"
                runat="server"
                Text="Customer Contact" />
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
                    Contact Type *
                </label>

                <div class="col-md-6">

                    <asp:DropDownList
                        ID="ddlContactType"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem
                            Text="Email"
                            Value="Email" />

                        <asp:ListItem
                            Text="Phone"
                            Value="Phone" />

                        <asp:ListItem
                            Text="Mobile"
                            Value="Mobile" />

                        <asp:ListItem
                            Text="WhatsApp"
                            Value="WhatsApp" />

                        <asp:ListItem
                            Text="Other"
                            Value="Other" />

                    </asp:DropDownList>

                </div>

            </div>

            <div class="form-group">

                <label class="control-label col-md-2">
                    Contact Value *
                </label>

                <div class="col-md-6">

                    <asp:TextBox
                        ID="txtContactValue"
                        runat="server"
                        CssClass="form-control" />

                    <asp:RequiredFieldValidator
                        ID="valContactValue"
                        runat="server"
                        ControlToValidate="txtContactValue"
                        ErrorMessage="Contact value is required."
                        CssClass="text-danger" />

                </div>

            </div>

            <div class="form-group">

                <label class="control-label col-md-2">
                    Primary
                </label>

                <div class="col-md-6">

                    <asp:CheckBox
                        ID="chkIsPrimary"
                        runat="server" />

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