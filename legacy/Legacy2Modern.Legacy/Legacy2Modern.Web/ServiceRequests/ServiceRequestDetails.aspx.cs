using System;
using Legacy2Modern.Business.Services;
using Legacy2Modern.Data;

namespace Legacy2Modern.Web.ServiceRequests
{
    public partial class ServiceRequestDetails
        : System.Web.UI.Page
    {
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
            }
            catch (Exception ex)
            {
                ShowError(
                    "Unable to load service request.");

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
    }
}