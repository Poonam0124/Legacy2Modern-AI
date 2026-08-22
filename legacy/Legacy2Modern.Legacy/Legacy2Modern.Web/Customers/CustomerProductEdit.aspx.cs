using System;
using Legacy2Modern.Business.Services;
using Legacy2Modern.Data;

namespace Legacy2Modern.Web.Customers
{
    public partial class CustomerProductEdit
        : System.Web.UI.Page
    {
        private CustomerService _customerService;

        private int CustomerProductId
        {
            get
            {
                int id;

                if (int.TryParse(
                    Request.QueryString["id"],
                    out id))
                {
                    return id;
                }

                return 0;
            }
        }

        private int CustomerId
        {
            get
            {
                int id;

                if (int.TryParse(
                    Request.QueryString["customerId"],
                    out id))
                {
                    return id;
                }

                return 0;
            }
        }

        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            _customerService =
                new CustomerService();

            if (!IsPostBack)
            {
                LoadProducts();

                if (CustomerProductId > 0)
                {
                    lblPageTitle.Text =
                        "Edit Customer Product";

                    LoadCustomerProduct();
                }
                else
                {
                    lblPageTitle.Text =
                        "Add Customer Product";
                }

                lnkCancel.NavigateUrl =
                    "CustomerDetails.aspx?id="
                    + CustomerId;
            }
        }

        private void LoadProducts()
        {
            ddlProduct.DataSource =
                _customerService.GetActiveProducts();

            ddlProduct.DataBind();

            ddlProduct.Items.Insert(
                0,
                new System.Web.UI.WebControls.ListItem(
                    "-- Select Product --",
                    ""));
        }

        private void LoadCustomerProduct()
        {
            var customerProduct =
                _customerService
                    .GetCustomerProductById(
                        CustomerProductId);

            if (customerProduct == null)
            {
                lblMessage.Text =
                    "Customer product not found.";

                btnSave.Enabled = false;

                return;
            }

            ddlProduct.SelectedValue =
                customerProduct.ProductId.ToString();

            txtSubscriptionNumber.Text =
                customerProduct.SubscriptionNumber;

            if (customerProduct.StartDate.HasValue)
            {
                txtStartDate.Text =
                    customerProduct.StartDate
                        .Value
                        .ToString("yyyy-MM-dd");
            }

            if (customerProduct.EndDate.HasValue)
            {
                txtEndDate.Text =
                    customerProduct.EndDate
                        .Value
                        .ToString("yyyy-MM-dd");
            }

            if (!string.IsNullOrEmpty(
                customerProduct.Status))
            {
                ddlStatus.SelectedValue =
                    customerProduct.Status;
            }

            lnkCancel.NavigateUrl =
                "CustomerDetails.aspx?id="
                + customerProduct.CustomerId;
        }

        protected void btnSave_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrEmpty(
                ddlProduct.SelectedValue))
            {
                lblMessage.Text =
                    "Please select a product.";

                return;
            }

            try
            {
                DateTime? startDate =
                    ParseDate(txtStartDate.Text);

                DateTime? endDate =
                    ParseDate(txtEndDate.Text);

                if (CustomerProductId == 0)
                {
                    if (CustomerId <= 0)
                    {
                        lblMessage.Text =
                            "Invalid customer.";

                        return;
                    }

                    var customerProduct =
                        new CustomerProduct
                        {
                            CustomerId = CustomerId,

                            ProductId =
                                Convert.ToInt32(
                                    ddlProduct.SelectedValue),

                            SubscriptionNumber =
                                txtSubscriptionNumber.Text
                                    .Trim(),

                            StartDate = startDate,

                            EndDate = endDate,

                            Status =
                                ddlStatus.SelectedValue
                        };

                    _customerService
                        .AddCustomerProduct(
                            customerProduct);

                }
                else
                {
                    var customerProduct =
                        _customerService
                            .GetCustomerProductById(
                                CustomerProductId);

                    if (customerProduct == null)
                    {
                        lblMessage.Text =
                            "Customer product not found.";

                        return;
                    }

                    customerProduct.ProductId =
                        Convert.ToInt32(
                            ddlProduct.SelectedValue);

                    customerProduct.SubscriptionNumber =
                        txtSubscriptionNumber.Text.Trim();

                    customerProduct.StartDate =
                        startDate;

                    customerProduct.EndDate =
                        endDate;

                    customerProduct.Status =
                        ddlStatus.SelectedValue;

                    _customerService
                        .UpdateCustomerProduct(
                            customerProduct);
                }

                Response.Redirect(
                    "CustomerDetails.aspx?id="
                    + (CustomerProductId > 0
                        ? _customerService
                            .GetCustomerProductById(
                                CustomerProductId)
                            .CustomerId
                        : CustomerId));
            }
            catch (Exception ex)
            {
                lblMessage.Text =
                    "Unable to save customer product.";

                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private DateTime? ParseDate(string value)
        {
            DateTime date;

            if (DateTime.TryParse(value, out date))
            {
                return date;
            }

            return null;
        }
    }
}