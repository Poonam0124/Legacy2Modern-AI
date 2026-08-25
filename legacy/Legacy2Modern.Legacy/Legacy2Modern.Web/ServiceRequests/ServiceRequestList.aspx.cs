using System;
using Legacy2Modern.Business.Services;

namespace Legacy2Modern.Web.ServiceRequests
{
    public partial class ServiceRequestList
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
                LoadServiceRequests();
            }
        }

        private void LoadServiceRequests()
        {
            try
            {
                var requests =
                    _serviceRequestService
                        .Search(
                            txtSearch.Text,
                            ddlStatus.SelectedValue,
                            ddlPriority.SelectedValue);

                gvServiceRequests.DataSource =
                    requests;

                gvServiceRequests.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text =
                    "Unable to load service requests.";

                System.Diagnostics.Debug
                    .WriteLine(ex.ToString());
            }
        }

        protected void btnSearch_Click(
            object sender,
            EventArgs e)
        {
            LoadServiceRequests();
        }
    }
}