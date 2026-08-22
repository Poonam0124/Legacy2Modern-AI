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
    <hr />

    <h3>Contacts</h3>

    <asp:Label
        ID="lblContactMessage"
        runat="server"
        CssClass="text-danger" />

    <br />

    <asp:HyperLink
        ID="lnkAddContact"
        runat="server"
        CssClass="btn btn-primary"
        Text="Add Contact" />

    <br />
    <br />

    <asp:GridView
        ID="gvContacts"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped">

        <Columns>

            <asp:BoundField
                DataField="ContactType"
                HeaderText="Type" />

            <asp:BoundField
                DataField="ContactValue"
                HeaderText="Contact" />

            <asp:CheckBoxField
                DataField="IsPrimary"
                HeaderText="Primary" />

            <asp:BoundField
                DataField="CreatedDate"
                HeaderText="Created"
                DataFormatString="{0:dd-MMM-yyyy}" />

            <asp:HyperLinkField
                Text="Edit"
                HeaderText="Action"
                DataNavigateUrlFields="CustomerContactId"
                DataNavigateUrlFormatString="CustomerContactEdit.aspx?id={0}"
                ControlStyle-CssClass="btn btn-sm btn-warning" />

        </Columns>

    </asp:GridView>

    <hr />

<h3>Products</h3>

<asp:Label
    ID="lblProductMessage"
    runat="server"
    CssClass="text-danger" />

<br />

<asp:HyperLink
    ID="lnkAddProduct"
    runat="server"
    CssClass="btn btn-primary"
    Text="Add Product" />

<br />
<br />

<asp:GridView
    ID="gvProducts"
    runat="server"
    AutoGenerateColumns="False"
    CssClass="table table-bordered table-striped">

    <Columns>

        <asp:BoundField
            DataField="Product.ProductCode"
            HeaderText="Product Code" />

        <asp:BoundField
            DataField="Product.ProductName"
            HeaderText="Product" />

        <asp:BoundField
            DataField="SubscriptionNumber"
            HeaderText="Subscription" />

        <asp:BoundField
            DataField="StartDate"
            HeaderText="Start Date"
            DataFormatString="{0:dd-MMM-yyyy}" />

        <asp:BoundField
            DataField="EndDate"
            HeaderText="End Date"
            DataFormatString="{0:dd-MMM-yyyy}" />

        <asp:BoundField
            DataField="Status"
            HeaderText="Status" />

        <asp:HyperLinkField
            Text="Edit"
            HeaderText="Action"
            DataNavigateUrlFields="CustomerProductId"
            DataNavigateUrlFormatString="CustomerProductEdit.aspx?id={0}"
            ControlStyle-CssClass="btn btn-sm btn-warning" />

    </Columns>

</asp:GridView>

</asp:Content>
