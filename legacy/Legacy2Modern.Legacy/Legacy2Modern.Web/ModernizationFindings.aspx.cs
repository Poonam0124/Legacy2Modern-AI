using System;
using Legacy2Modern.Business.Services;

namespace Legacy2Modern.Web
{
    public partial class ModernizationFindings : System.Web.UI.Page
    {
        private readonly ModernizationFindingService _findingService =
            new ModernizationFindingService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFindings();
            }
        }

        private void LoadFindings()
        {
            var findings = _findingService.GetLegacyFindings();

            gvFindings.DataSource = findings;
            gvFindings.DataBind();
        }
    }
}