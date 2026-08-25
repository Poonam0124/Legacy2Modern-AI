using System;
using System.Linq;
using Legacy2Modern.Business.Services;

namespace Legacy2Modern.Web.ServiceRequests
{
    public partial class ServiceRequestCreate
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
                LoadCustomers();
                LoadEmployees();
                LoadRequestTypes();
                LoadCustomerProducts();
            }
        }

        private void LoadCustomers()
        {
            var customers =
                _serviceRequestService
                    .GetActiveCustomers();

            ddlCustomer.DataSource = customers;

            ddlCustomer.DataTextField =
                "CustomerCode";

            ddlCustomer.DataValueField =
                "CustomerId";

            ddlCustomer.DataBind();

            ddlCustomer.Items.Insert(
                0,
                new System.Web.UI.WebControls.ListItem(
                    "-- Select Customer --",
                    ""));
        }

        private void LoadEmployees()
        {
            var employees =
                _serviceRequestService
                    .GetActiveEmployees();

            ddlAssignedTo.DataSource = employees;

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

        private void LoadRequestTypes()
        {
            // Request types are currently defined
            // in the ASPX markup.
        }

        private void LoadCustomerProducts()
        {
            ddlCustomerProduct.Items.Clear();

            ddlCustomerProduct.Items.Add(
                new System.Web.UI.WebControls.ListItem(
                    "-- Select Product --",
                    ""));
        }

        protected void ddlCustomer_SelectedIndexChanged(
     object sender,
     EventArgs e)
        {
            ddlCustomerProduct.Items.Clear();

            if (string.IsNullOrWhiteSpace(
                ddlCustomer.SelectedValue))
            {
                ddlCustomerProduct.Items.Add(
                    new System.Web.UI.WebControls.ListItem(
                        "-- Select Product --",
                        ""));

                return;
            }

            int customerId =
                Convert.ToInt32(
                    ddlCustomer.SelectedValue);

            LoadCustomerProductsForCustomer(
                customerId);
        }

        private void LoadCustomerProductsForCustomer(
     int customerId)
        {
            var products =
                _serviceRequestService
                    .GetCustomerProducts(customerId)
                    .Select(x => new
                    {
                        x.CustomerProductId,
                        DisplayName =
                            x.SubscriptionNumber
                            + " - "
                            + x.Product.ProductName
                    })
                    .ToList();

            ddlCustomerProduct.DataSource = products;

            ddlCustomerProduct.DataTextField =
                "DisplayName";

            ddlCustomerProduct.DataValueField =
                "CustomerProductId";

            ddlCustomerProduct.DataBind();

            ddlCustomerProduct.Items.Insert(
                0,
                new System.Web.UI.WebControls.ListItem(
                    "-- Select Product --",
                    ""));
        }

        protected void btnSave_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                lblMessage.Text = "";

                if (string.IsNullOrWhiteSpace(
                    ddlCustomer.SelectedValue))
                {
                    lblMessage.Text =
                        "Please select a customer.";

                    return;
                }

                if (string.IsNullOrWhiteSpace(
                    ddlRequestType.SelectedValue))
                {
                    lblMessage.Text =
                        "Please select a request type.";

                    return;
                }

                if (string.IsNullOrWhiteSpace(
                    ddlPriority.SelectedValue))
                {
                    lblMessage.Text =
                        "Please select a priority.";

                    return;
                }

                int customerId =
                    Convert.ToInt32(
                        ddlCustomer.SelectedValue);

                int? customerProductId = null;

                if (!string.IsNullOrWhiteSpace(
                    ddlCustomerProduct.SelectedValue))
                {
                    customerProductId =
                        Convert.ToInt32(
                            ddlCustomerProduct.SelectedValue);
                }

                int? assignedToEmployeeId = null;

                if (!string.IsNullOrWhiteSpace(
                    ddlAssignedTo.SelectedValue))
                {
                    assignedToEmployeeId =
                        Convert.ToInt32(
                            ddlAssignedTo.SelectedValue);
                }

                int requestId =
                    _serviceRequestService
                        .CreateServiceRequest(
                            customerId,
                            customerProductId,
                            assignedToEmployeeId,
                            txtSubject.Text,
                            txtDescription.Text,
                            ddlRequestType.SelectedValue,
                            ddlPriority.SelectedValue,
                            null);

                Response.Redirect(
                    "ServiceRequestDetails.aspx?id="
                    + requestId);
            }
            catch (Exception ex)
            {
                lblMessage.Text =
                    "Unable to create service request: "
                    + ex.Message;

                System.Diagnostics.Debug
                    .WriteLine(ex.ToString());
            }
        }
    }
}