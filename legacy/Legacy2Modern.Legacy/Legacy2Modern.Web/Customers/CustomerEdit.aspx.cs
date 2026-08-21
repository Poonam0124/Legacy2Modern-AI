using System;
using Legacy2Modern.Business.Services;
using Legacy2Modern.Data;

namespace Legacy2Modern.Web.Customers
{
    public partial class CustomerEdit : System.Web.UI.Page
    {
        private CustomerService _customerService;

        private int CustomerId
        {
            get
            {
                int id;

                if (int.TryParse(Request.QueryString["id"], out id))
                {
                    return id;
                }

                return 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _customerService = new CustomerService();

            if (!IsPostBack)
            {
                if (CustomerId > 0)
                {
                    lblPageTitle.Text = "Edit Customer";
                    LoadCustomer();
                }
                else
                {
                    lblPageTitle.Text = "Add Customer";
                }
            }
        }

        private void LoadCustomer()
        {
            var customer =
                _customerService.GetCustomerById(CustomerId);

            if (customer == null)
            {
                lblMessage.Text = "Customer not found.";
                btnSave.Enabled = false;
                return;
            }

            txtCustomerCode.Text = customer.CustomerCode;
            txtFirstName.Text = customer.FirstName;
            txtLastName.Text = customer.LastName;
            txtEmail.Text = customer.Email;
            txtPhone.Text = customer.Phone;

            if (!string.IsNullOrEmpty(customer.Status))
            {
                ddlStatus.SelectedValue = customer.Status;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            var customer = new Customer
            {
                CustomerId = CustomerId,
                CustomerCode = txtCustomerCode.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Status = ddlStatus.SelectedValue
            };

            try
            {
                if (CustomerId == 0)
                {
                    _customerService.AddCustomer(customer);
                }
                else
                {
                    _customerService.UpdateCustomer(customer);
                }

                Response.Redirect("CustomerList.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text =
                    "Unable to save customer.";

                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}