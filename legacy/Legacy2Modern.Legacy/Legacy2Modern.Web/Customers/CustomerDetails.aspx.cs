using System;
using Legacy2Modern.Business.Services;

namespace Legacy2Modern.Web.Customers
{
    public partial class CustomerDetails : System.Web.UI.Page
    {
        private CustomerService _customerService;

        protected void Page_Load(object sender, EventArgs e)
        {
            _customerService = new CustomerService();

            if (!IsPostBack)
            {
                LoadCustomer();
            }
        }

        private void LoadCustomer()
        {
            int customerId;

            if (!int.TryParse(Request.QueryString["id"], out customerId))
            {
                lblMessage.Text = "Invalid customer.";
                return;
            }

            var customer =
                _customerService.GetCustomerById(customerId);

            if (customer == null)
            {
                lblMessage.Text = "Customer not found.";
                return;
            }

            lblCustomerCode.Text = customer.CustomerCode;
            lblFirstName.Text = customer.FirstName;
            lblLastName.Text = customer.LastName;
            lblEmail.Text = customer.Email;
            lblPhone.Text = customer.Phone;
            lblStatus.Text = customer.Status;

            lnkEdit.NavigateUrl =
                "CustomerEdit.aspx?id=" + customer.CustomerId;
        }
    }
}