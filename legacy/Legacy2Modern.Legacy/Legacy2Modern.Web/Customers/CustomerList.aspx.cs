using System;
using System.Linq;
using Legacy2Modern.Business.Services;

namespace Legacy2Modern.Web.Customers
{
    public partial class CustomerList : System.Web.UI.Page
    {
        private CustomerService _customerService;

        protected void Page_Load(object sender, EventArgs e)
        {
            _customerService = new CustomerService();

            if (!IsPostBack)
            {
                LoadCustomers();
            }
        }

        private void LoadCustomers()
        {
            try
            {
                var customers = _customerService.GetAllCustomers();

                gvCustomers.DataSource = customers;
                gvCustomers.DataBind();

                lblMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Unable to load customers.";

                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var searchText = txtSearch.Text.Trim();

            var customers = _customerService
                .GetAllCustomers();

            if (!string.IsNullOrEmpty(searchText))
            {
                customers = customers
                    .Where(x =>
                        (x.FirstName != null &&
                         x.FirstName.Contains(searchText))
                        ||
                        (x.LastName != null &&
                         x.LastName.Contains(searchText))
                        ||
                        (x.Email != null &&
                         x.Email.Contains(searchText))
                        ||
                        (x.CustomerCode != null &&
                         x.CustomerCode.Contains(searchText)))
                    .ToList();
            }

            gvCustomers.DataSource = customers;
            gvCustomers.DataBind();
        }
    }
}