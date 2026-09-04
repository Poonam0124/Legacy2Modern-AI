using System;
using System.Web.Script.Serialization;
using Legacy2Modern.Business.Services;

namespace Legacy2Modern.Web
{
    public partial class ModernizationExport : System.Web.UI.Page
    {
        private readonly ModernizationFindingService _findingService =
            new ModernizationFindingService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ExportFindings();
            }
        }

        private void ExportFindings()
        {
            var exportData =
                _findingService.GetExportData();

            var serializer =
                new JavaScriptSerializer();

            litJson.Text =
                Server.HtmlEncode(
                    serializer.Serialize(exportData));
        }
    }
}