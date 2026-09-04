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
                LoadSummary();
                LoadFindings();
            }
        }

        private void LoadFindings()
        {
            var findings = _findingService.GetLegacyFindings();

            gvFindings.DataSource = findings;
            gvFindings.DataBind();
        }

        private void LoadSummary()
        {
            var summary = _findingService.GetSummary();

            lblTotalFindings.Text =
                summary.TotalFindings.ToString();

            lblHighRisk.Text =
                summary.HighOrCriticalRiskCount.ToString();

            lblHighPriority.Text =
                summary.HighOrCriticalPriorityCount.ToString();

            lblIdentified.Text =
                summary.IdentifiedCount.ToString();

            lblEffortDistribution.Text =
                string.Format(
                    "Low: {0} | Medium: {1} | High: {2} | Very High: {3}",
                    summary.LowEffortCount,
                    summary.MediumEffortCount,
                    summary.HighEffortCount,
                    summary.VeryHighEffortCount);
        }
    }
}