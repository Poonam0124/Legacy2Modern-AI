<%@ Page Title="Customers"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="CustomerList.aspx.cs"
    Inherits="Legacy2Modern.Web.Customers.CustomerList" %>

<asp:Content ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="container">

        <h2>Customer Management</h2>

        <hr />

        <div class="row">

            <div class="col-md-6">

                <asp:TextBox
                    ID="txtSearch"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Search customer..." />

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
            ID="gvCustomers"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped"
            EmptyDataText="No customers found.">

            <Columns>

                <asp:BoundField
                    DataField="CustomerCode"
                    HeaderText="Customer Code" />

                <asp:BoundField
                    DataField="FirstName"
                    HeaderText="First Name" />

                <asp:BoundField
                    DataField="LastName"
                    HeaderText="Last Name" />

                <asp:BoundField
                    DataField="Email"
                    HeaderText="Email" />

                <asp:BoundField
                    DataField="Phone"
                    HeaderText="Phone" />

                <asp:BoundField
                    DataField="Status"
                    HeaderText="Status" />

                <asp:HyperLinkField
                    Text="View"
                    HeaderText="Action"
                    DataNavigateUrlFields="CustomerId"
                    DataNavigateUrlFormatString="CustomerDetails.aspx?id={0}"
                    ControlStyle-CssClass="btn btn-sm btn-info" />

                <asp:HyperLinkField
                    Text="Edit"
                    HeaderText=""
                    DataNavigateUrlFields="CustomerId"
                    DataNavigateUrlFormatString="CustomerEdit.aspx?id={0}"
                    ControlStyle-CssClass="btn btn-sm btn-warning" />

            </Columns>

        </asp:GridView>

    </div>

</asp:Content>
