<%@ Page Title="Create Service Request"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="ServiceRequestCreate.aspx.cs"
    Inherits="Legacy2Modern.Web.ServiceRequests.ServiceRequestCreate" %>

<asp:Content
    ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="container">

        <h2>Create Service Request</h2>

        <hr />

        <asp:Label
            ID="lblMessage"
            runat="server"
            CssClass="text-danger" />

        <div class="form-horizontal">

            <!-- Customer -->

            <div class="form-group">
                <label class="control-label col-md-2">
                    Customer
                </label>

                <div class="col-md-6">

                    <asp:DropDownList
                        ID="ddlCustomer"
                        runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">

                    </asp:DropDownList>

                </div>
            </div>

            <!-- Customer Product -->

            <div class="form-group">

                <label class="control-label col-md-2">
                    Product
                </label>

                <div class="col-md-6">

                    <asp:DropDownList
                        ID="ddlCustomerProduct"
                        runat="server"
                        CssClass="form-control">

                    </asp:DropDownList>

                </div>

            </div>

            <!-- Request Type -->

            <div class="form-group">

                <label class="control-label col-md-2">
                    Request Type
                </label>

                <div class="col-md-6">

                    <asp:DropDownList
                        ID="ddlRequestType"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem
                            Text="-- Select Type --"
                            Value="" />

                        <asp:ListItem
                            Text="Technical"
                            Value="Technical" />

                        <asp:ListItem
                            Text="Billing"
                            Value="Billing" />

                        <asp:ListItem
                            Text="General"
                            Value="General" />

                        <asp:ListItem
                            Text="Account"
                            Value="Account" />

                    </asp:DropDownList>

                </div>

            </div>

            <!-- Priority -->

            <div class="form-group">

                <label class="control-label col-md-2">
                    Priority
                </label>

                <div class="col-md-6">

                    <asp:DropDownList
                        ID="ddlPriority"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem
                            Text="-- Select Priority --"
                            Value="" />

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

            </div>

            <!-- Assigned Employee -->

            <div class="form-group">

                <label class="control-label col-md-2">
                    Assigned To
                </label>

                <div class="col-md-6">

                    <asp:DropDownList
                        ID="ddlAssignedTo"
                        runat="server"
                        CssClass="form-control">

                    </asp:DropDownList>

                </div>

            </div>

            <!-- Subject -->

            <div class="form-group">

                <label class="control-label col-md-2">
                    Subject
                </label>

                <div class="col-md-6">

                    <asp:TextBox
                        ID="txtSubject"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="200" />

                </div>

            </div>

            <!-- Description -->

            <div class="form-group">

                <label class="control-label col-md-2">
                    Description
                </label>

                <div class="col-md-6">

                    <asp:TextBox
                        ID="txtDescription"
                        runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine"
                        Rows="5" />

                </div>

            </div>

            <br />

            <div class="form-group">

                <div class="col-md-offset-2 col-md-6">

                    <asp:Button
                        ID="btnSave"
                        runat="server"
                        Text="Create Request"
                        CssClass="btn btn-primary"
                        OnClick="btnSave_Click" />

                    <a
                        href="ServiceRequestList.aspx"
                        class="btn btn-default">
                        Cancel
                    </a>

                </div>

            </div>

        </div>

    </div>

</asp:Content>