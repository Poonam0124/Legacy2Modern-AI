<%@ Page Title="Service Request Details"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="ServiceRequestDetails.aspx.cs"
    Inherits="Legacy2Modern.Web.ServiceRequests.ServiceRequestDetails" %>

<asp:Content
    ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="container">

        <h2>Service Request Details
        </h2>

        <hr />

        <asp:Label
            ID="lblMessage"
            runat="server"
            CssClass="text-danger" />

        <asp:Panel
            ID="pnlDetails"
            runat="server">

            <div class="row">

                <div class="col-md-6">

                    <table class="table table-bordered">

                        <tr>
                            <th>Request Number</th>
                            <td>
                                <asp:Label
                                    ID="lblRequestNumber"
                                    runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <th>Customer</th>
                            <td>
                                <asp:Label
                                    ID="lblCustomer"
                                    runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <th>Product</th>
                            <td>
                                <asp:Label
                                    ID="lblProduct"
                                    runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <th>Request Type</th>
                            <td>
                                <asp:Label
                                    ID="lblRequestType"
                                    runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <th>Priority</th>
                            <td>
                                <asp:Label
                                    ID="lblPriority"
                                    runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <th>Status</th>
                            <td>

                                <asp:Label
                                    ID="lblStatus"
                                    runat="server" />

                                <br />
                                <br />

                                <label>Change Status</label>

                                <asp:DropDownList
                                    ID="ddlStatus"
                                    runat="server"
                                    CssClass="form-control"
                                    Style="max-width: 350px;">
                                </asp:DropDownList>

                                <br />

                                <label>Change Reason</label>

                                <asp:TextBox
                                    ID="txtChangeReason"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="MultiLine"
                                    Rows="3"
                                    MaxLength="500"
                                    Style="max-width: 500px;" />

                                <br />

                                <asp:Button
                                    ID="btnChangeStatus"
                                    runat="server"
                                    Text="Change Status"
                                    CssClass="btn btn-primary"
                                    OnClick="btnChangeStatus_Click" />

                                &nbsp;

                            <asp:Label
                                ID="lblStatusMessage"
                                runat="server" />

                            </td>
                        </tr>

                        <tr>
                            <th>Assigned To</th>
                            <td>

                                <asp:Label
                                    ID="lblAssignedTo"
                                    runat="server" />

                                <br />

                                <asp:DropDownList
                                    ID="ddlAssignedTo"
                                    runat="server"
                                    CssClass="form-control"
                                    Style="margin-top: 8px;">
                                </asp:DropDownList>

                                <br />

                                <asp:Button
                                    ID="btnAssignEmployee"
                                    runat="server"
                                    Text="Save Assignment"
                                    CssClass="btn btn-primary"
                                    OnClick="btnAssignEmployee_Click" />

                                <asp:Label
                                    ID="lblAssignmentMessage"
                                    runat="server"
                                    CssClass="text-success" />

                            </td>
                        </tr>

                        <tr>
                            <th>Created Date</th>
                            <td>
                                <asp:Label
                                    ID="lblCreatedDate"
                                    runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <th>Modified Date</th>
                            <td>
                                <asp:Label
                                    ID="lblModifiedDate"
                                    runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <th>Closed Date</th>
                            <td>
                                <asp:Label
                                    ID="lblClosedDate"
                                    runat="server" />
                            </td>
                        </tr>

                    </table>

                </div>

            </div>

            <div class="row">

                <div class="col-md-8">

                    <h4>Subject</h4>

                    <p>
                        <asp:Label
                            ID="lblSubject"
                            runat="server" />
                    </p>

                    <h4>Description</h4>

                    <div class="well">

                        <asp:Label
                            ID="lblDescription"
                            runat="server" />

                    </div>

                </div>

            </div>

            <br />

            <a
                href="ServiceRequestList.aspx"
                class="btn btn-default">Back to Requests
            </a>

        </asp:Panel>

        <hr />

        <h3>Comments</h3>

        <asp:Repeater
            ID="rptComments"
            runat="server">

            <ItemTemplate>

                <div style="margin-bottom: 20px; padding: 12px; border: 1px solid #ddd; border-radius: 4px;">

                    <strong>
                        <%# GetEmployeeName(
                    Eval("Employee")) %>
                    </strong>

                    <span style="color: #777;">-
                <%# Eval(
                    "CreatedDate",
                    "{0:dd-MMM-yyyy HH:mm}") %>
                    </span>

                    <br />

                    <div style="margin-top: 8px;">
                        <%# Server.HtmlEncode(
                    Eval("CommentText").ToString()) %>
                    </div>

                </div>

            </ItemTemplate>

            <FooterTemplate>

                <asp:Label
                    ID="lblNoComments"
                    runat="server"
                    Text="No comments yet."
                    Visible="false" />

            </FooterTemplate>

        </asp:Repeater>

        <hr />

        <h4>Add Comment</h4>

        <label>Comment As</label>

        <asp:DropDownList
            ID="ddlCommentEmployee"
            runat="server"
            CssClass="form-control"
            Style="max-width: 350px;">
        </asp:DropDownList>

        <br />

        <asp:TextBox
            ID="txtComment"
            runat="server"
            CssClass="form-control"
            TextMode="MultiLine"
            Rows="4"
            MaxLength="5000"
            Style="max-width: 700px;" />

        <br />

        <asp:Button
            ID="btnAddComment"
            runat="server"
            Text="Add Comment"
            CssClass="btn btn-primary"
            OnClick="btnAddComment_Click" />

        &nbsp;

        <asp:Label
            ID="lblCommentMessage"
            runat="server" />
    </div>

</asp:Content>
