using Legacy2Modern.Business.Services;
using Legacy2Modern.Data;
using System;
using System.Linq;

namespace Legacy2Modern.Web.ServiceRequests
{
    public partial class ServiceRequestDetails
        : System.Web.UI.Page
    {
        private const string StatusOpen =
    "Open";

        private const string StatusAssigned =
            "Assigned";

        private const string StatusInProgress =
            "In Progress";

        private const string StatusResolved =
            "Resolved";

        private const string StatusClosed =
            "Closed";

        private ServiceRequestService
            _serviceRequestService;

        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            _serviceRequestService =
                new ServiceRequestService();

            if (!IsPostBack)
            {
                LoadEmployees();
                LoadServiceRequest();
                LoadCommentEmployees();
            }
        }

        private void LoadServiceRequest()
        {
            try
            {
                string idValue =
                    Request.QueryString["id"];

                int serviceRequestId;

                if (!int.TryParse(
                    idValue,
                    out serviceRequestId))
                {
                    ShowError(
                        "Invalid service request.");
                    return;
                }

                var request =
                    _serviceRequestService
                        .GetById(serviceRequestId);

                if (request == null)
                {
                    ShowError(
                        "Service request not found.");
                    return;
                }

                DisplayRequest(request);
                LoadComments(serviceRequestId);
            }
            catch (Exception ex)
            {
                ShowError(
                    "Unable to load service request.");

                System.Diagnostics.Debug
                    .WriteLine(ex.ToString());
            }
        }

        private void LoadComments(int serviceRequestId)
        {
            try
            {
                var comments =
                    _serviceRequestService
                        .GetComments(serviceRequestId);

                rptComments.DataSource =
                    comments;

                rptComments.DataBind();
            }
            catch (Exception ex)
            {
                lblCommentMessage.Text =
                    "Unable to load comments.";

                lblCommentMessage.CssClass =
                    "text-danger";

                System.Diagnostics.Debug
                    .WriteLine(ex.ToString());
            }
        }
        protected string GetEmployeeName(
    object employeeObject)
        {
            var employee =
                employeeObject as Employee;

            if (employee == null)
            {
                return "Unknown Employee";
            }

            return string.Format(
                "{0} {1}",
                employee.FirstName,
                employee.LastName)
                .Trim();
        }
        protected void btnAddComment_Click(
    object sender,
    EventArgs e)
        {
            try
            {
                lblCommentMessage.Text = "";
                lblCommentMessage.CssClass =
                    "text-success";

                int serviceRequestId;

                if (!int.TryParse(
                    Request.QueryString["id"],
                    out serviceRequestId))
                {
                    lblCommentMessage.Text =
                        "Invalid service request.";

                    lblCommentMessage.CssClass =
                        "text-danger";

                    return;
                }

                string commentText =
                    txtComment.Text.Trim();

                if (string.IsNullOrWhiteSpace(
                    commentText))
                {
                    lblCommentMessage.Text =
                        "Please enter a comment.";

                    lblCommentMessage.CssClass =
                        "text-danger";

                    return;
                }

                if (commentText.Length > 5000)
                {
                    lblCommentMessage.Text =
                        "Comment cannot exceed 5000 characters.";

                    lblCommentMessage.CssClass =
                        "text-danger";

                    return;
                }

                int employeeId;

                if (!int.TryParse(
                    ddlCommentEmployee.SelectedValue,
                    out employeeId))
                {
                    lblCommentMessage.Text =
                        "Please select an employee.";

                    lblCommentMessage.CssClass =
                        "text-danger";

                    return;
                }

                _serviceRequestService.AddComment(
                    serviceRequestId,
                    employeeId,
                    commentText);

                txtComment.Text = "";

                LoadComments(serviceRequestId);

                lblCommentMessage.Text =
                    "Comment added successfully.";
            }
            catch (Exception ex)
            {
                lblCommentMessage.Text =
                    "Unable to add comment.";

                lblCommentMessage.CssClass =
                    "text-danger";

                System.Diagnostics.Debug
                    .WriteLine(ex.ToString());
            }
        }
        private void DisplayRequest(
            ServiceRequest request)
        {
            lblRequestNumber.Text =
                request.RequestNumber;

            lblRequestType.Text =
                request.RequestType;

            lblPriority.Text =
                request.Priority;

            lblStatus.Text =
                request.Status;

            LoadAvailableStatuses(
    request.Status);

            lblSubject.Text =
                request.Subject;

            lblDescription.Text =
                string.IsNullOrWhiteSpace(
                    request.Description)
                    ? "No description provided."
                    : request.Description;

            lblCreatedDate.Text =
                request.CreatedDate
                    .ToString("dd-MMM-yyyy HH:mm");

            lblModifiedDate.Text =
                request.ModifiedDate.HasValue
                    ? request.ModifiedDate.Value
                        .ToString("dd-MMM-yyyy HH:mm")
                    : "-";

            lblClosedDate.Text =
                request.ClosedDate.HasValue
                    ? request.ClosedDate.Value
                        .ToString("dd-MMM-yyyy HH:mm")
                    : "-";

            if (request.Customer != null)
            {
                lblCustomer.Text =
                    request.Customer.CustomerCode
                    + " - "
                    + request.Customer.FirstName
                    + " "
                    + request.Customer.LastName;
            }
            else
            {
                lblCustomer.Text = "-";
            }

            if (request.CustomerProduct != null)
            {
                var product =
                    request.CustomerProduct.Product;

                if (product != null)
                {
                    lblProduct.Text =
                        request.CustomerProduct
                            .SubscriptionNumber
                        + " - "
                        + product.ProductName;
                }
                else
                {
                    lblProduct.Text =
                        request.CustomerProduct
                            .SubscriptionNumber;
                }
            }
            else
            {
                lblProduct.Text = "-";
            }

            if (request.Employee != null)
            {
                lblAssignedTo.Text =
                    request.Employee.EmployeeCode
                    + " - "
                    + request.Employee.FirstName
                    + " "
                    + request.Employee.LastName;

                if (ddlAssignedTo.Items.FindByValue(
                    request.Employee.EmployeeId.ToString())
                    != null)
                {
                    ddlAssignedTo.SelectedValue =
                        request.Employee.EmployeeId.ToString();
                }
            }
            else
            {
                lblAssignedTo.Text =
                    "Unassigned";

                ddlAssignedTo.SelectedIndex = 0;
            }
        }

        private void ShowError(string message)
        {
            pnlDetails.Visible = false;
            lblMessage.Text = message;
        }

        private void LoadEmployees()
        {
            var employees =
                _serviceRequestService
                    .GetActiveEmployees();

            ddlAssignedTo.DataSource =
                employees;

            ddlAssignedTo.DataTextField =
                "FirstName";

            ddlAssignedTo.DataValueField =
                "EmployeeId";

            ddlAssignedTo.DataBind();

            ddlAssignedTo.Items.Insert(
                0,
                new System.Web.UI.WebControls.ListItem(
                    "-- Unassigned --",
                    ""));
        }

        protected void btnAssignEmployee_Click(
    object sender,
    EventArgs e)
        {
            try
            {
                lblAssignmentMessage.Text = "";

                int serviceRequestId;

                if (!int.TryParse(
                    Request.QueryString["id"],
                    out serviceRequestId))
                {
                    lblAssignmentMessage.Text =
                        "Invalid service request.";

                    return;
                }

                int? employeeId = null;

                if (!string.IsNullOrWhiteSpace(
                    ddlAssignedTo.SelectedValue))
                {
                    employeeId =
                        Convert.ToInt32(
                            ddlAssignedTo.SelectedValue);
                }

                _serviceRequestService
                    .AssignEmployee(
                        serviceRequestId,
                        employeeId);

                lblAssignmentMessage.Text =
                    "Assignment saved successfully.";

                LoadServiceRequest();
            }
            catch (Exception ex)
            {
                lblAssignmentMessage.Text =
                    "Unable to save assignment.";

                System.Diagnostics.Debug
                    .WriteLine(ex.ToString());
            }
        }

        private void LoadAvailableStatuses(
    string currentStatus)
        {
            ddlStatus.Items.Clear();

            switch (currentStatus)
            {
                case StatusOpen:

                    ddlStatus.Items.Add(
                        new System.Web.UI.WebControls.ListItem(
                            StatusAssigned,
                            StatusAssigned));

                    break;

                case StatusAssigned:

                    ddlStatus.Items.Add(
                        new System.Web.UI.WebControls.ListItem(
                            StatusOpen,
                            StatusOpen));

                    ddlStatus.Items.Add(
                        new System.Web.UI.WebControls.ListItem(
                            StatusInProgress,
                            StatusInProgress));

                    break;

                case StatusInProgress:

                    ddlStatus.Items.Add(
                        new System.Web.UI.WebControls.ListItem(
                            StatusAssigned,
                            StatusAssigned));

                    ddlStatus.Items.Add(
                        new System.Web.UI.WebControls.ListItem(
                            StatusResolved,
                            StatusResolved));

                    break;

                case StatusResolved:

                    ddlStatus.Items.Add(
                        new System.Web.UI.WebControls.ListItem(
                            StatusInProgress,
                            StatusInProgress));

                    ddlStatus.Items.Add(
                        new System.Web.UI.WebControls.ListItem(
                            StatusClosed,
                            StatusClosed));

                    break;

                case StatusClosed:

                    ddlStatus.Items.Add(
                        new System.Web.UI.WebControls.ListItem(
                            "No further transitions",
                            ""));

                    btnChangeStatus.Enabled = false;

                    break;
            }
        }

        protected void btnChangeStatus_Click(
    object sender,
    EventArgs e)
        {
            try
            {
                lblStatusMessage.Text = "";
                lblStatusMessage.CssClass =
                    "text-success";

                int serviceRequestId;

                if (!int.TryParse(
                    Request.QueryString["id"],
                    out serviceRequestId))
                {
                    lblStatusMessage.Text =
                        "Invalid service request.";

                    lblStatusMessage.CssClass =
                        "text-danger";

                    return;
                }

                if (string.IsNullOrWhiteSpace(
                    ddlStatus.SelectedValue))
                {
                    lblStatusMessage.Text =
                        "No valid status transition is available.";

                    lblStatusMessage.CssClass =
                        "text-danger";

                    return;
                }

                string changeReason =
                    txtChangeReason.Text.Trim();

                if (string.IsNullOrWhiteSpace(
                    changeReason))
                {
                    lblStatusMessage.Text =
                        "Please enter a change reason.";

                    lblStatusMessage.CssClass =
                        "text-danger";

                    return;
                }

                _serviceRequestService
                    .ChangeStatus(
                        serviceRequestId,
                        ddlStatus.SelectedValue,
                        null,
                        changeReason);

                lblStatusMessage.Text =
                    "Status changed successfully.";

                txtChangeReason.Text = "";

                LoadServiceRequest();
            }
            catch (Exception ex)
            {
                lblStatusMessage.Text =
                    ex.Message;

                lblStatusMessage.CssClass =
                    "text-danger";

                System.Diagnostics.Debug
                    .WriteLine(ex.ToString());
            }
        }

        private void LoadCommentEmployees()
        {
            var employees =
                _serviceRequestService
                    .GetActiveEmployees();

            ddlCommentEmployee.DataSource =
                employees.Select(x => new
                {
                    EmployeeId = x.EmployeeId,

                    DisplayName =
                        x.EmployeeCode
                        + " - "
                        + x.FirstName
                        + " "
                        + x.LastName
                }).ToList();

            ddlCommentEmployee.DataTextField =
                "DisplayName";

            ddlCommentEmployee.DataValueField =
                "EmployeeId";

            ddlCommentEmployee.DataBind();
        }
    }
}