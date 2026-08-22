using System;
using Legacy2Modern.Business.Services;
using Legacy2Modern.Data;

namespace Legacy2Modern.Web.Customers
{
    public partial class CustomerContactEdit
        : System.Web.UI.Page
    {
        private CustomerService _customerService;

        private int ContactId
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

        protected void Page_Load(object sender, EventArgs e)
        {
            _customerService =
                new CustomerService();

            if (!IsPostBack)
            {
                if (ContactId > 0)
                {
                    lblPageTitle.Text =
                        "Edit Customer Contact";

                    LoadContact();
                }
                else
                {
                    lblPageTitle.Text =
                        "Add Customer Contact";
                }

                lnkCancel.NavigateUrl =
                    "CustomerDetails.aspx?id="
                    + CustomerId;
            }
        }

        private void LoadContact()
        {
            var contact =
                _customerService
                    .GetCustomerContactById(ContactId);

            if (contact == null)
            {
                lblMessage.Text =
                    "Contact not found.";

                btnSave.Enabled = false;
                return;
            }

            ddlContactType.SelectedValue =
                contact.ContactType;

            txtContactValue.Text =
                contact.ContactValue;

            chkIsPrimary.Checked =
                contact.IsPrimary;

            lnkCancel.NavigateUrl =
                "CustomerDetails.aspx?id="
                + contact.CustomerId;
        }

        protected void btnSave_Click(
            object sender,
            EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                if (ContactId == 0)
                {
                    if (CustomerId <= 0)
                    {
                        lblMessage.Text =
                            "Invalid customer.";

                        return;
                    }

                    var contact =
                        new CustomerContact
                        {
                            CustomerId = CustomerId,
                            ContactType =
                                ddlContactType.SelectedValue,
                            ContactValue =
                                txtContactValue.Text.Trim(),
                            IsPrimary =
                                chkIsPrimary.Checked
                        };

                    _customerService
                        .AddCustomerContact(contact);
                }
                else
                {
                    var contact =
                        _customerService
                            .GetCustomerContactById(ContactId);

                    if (contact == null)
                    {
                        lblMessage.Text =
                            "Contact not found.";

                        return;
                    }

                    contact.ContactType =
                        ddlContactType.SelectedValue;

                    contact.ContactValue =
                        txtContactValue.Text.Trim();

                    contact.IsPrimary =
                        chkIsPrimary.Checked;

                    _customerService
                        .UpdateCustomerContact(contact);
                }

                Response.Redirect(
                    "CustomerDetails.aspx?id="
                    + (ContactId > 0
                        ? _customerService
                            .GetCustomerContactById(ContactId)
                            .CustomerId
                        : CustomerId));
            }
            catch (Exception ex)
            {
                lblMessage.Text =
                    "Unable to save contact.";

                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}