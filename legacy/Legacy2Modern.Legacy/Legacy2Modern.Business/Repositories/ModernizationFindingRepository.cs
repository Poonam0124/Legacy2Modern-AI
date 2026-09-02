using System.Collections.Generic;
using Legacy2Modern.Business.Models;

namespace Legacy2Modern.Business.Repositories
{
    public class ModernizationFindingRepository
        : IModernizationFindingRepository
    {
        public List<ModernizationFinding> GetAll()
        {
            return new List<ModernizationFinding>
            {
                new ModernizationFinding
                {
                    Id = "M3.2",
                    Category = "Business Rules",
                    Title = "Hard-coded workflow rules",
                    Description =
                        "Workflow behavior is represented through hard-coded rules.",
                    Evidence =
                        "Workflow-related status and transition logic is implemented directly in application code.",
                    Risk = "Medium",
                    Recommendation =
                        "Centralize workflow rules behind a dedicated business/domain rule component.",
                    Priority = "High"
                },

                new ModernizationFinding
                {
                    Id = "M3.3",
                    Category = "Business Rules",
                    Title = "Scattered status rules",
                    Description =
                        "Status values and status-related conditions are implemented in multiple locations.",
                    Evidence =
                        "Status comparisons such as 'Active', 'Open', and 'In Progress' are represented as hard-coded values.",
                    Risk = "Medium",
                    Recommendation =
                        "Centralize status definitions and business transitions.",
                    Priority = "High"
                },

                new ModernizationFinding
                {
                    Id = "M3.4",
                    Category = "Architecture",
                    Title = "Web directly depends on Data",
                    Description =
                        "The Web layer directly references the Data layer and EF6 entities.",
                    Evidence =
                        "WebForms code-behind directly uses Customer, CustomerContact, CustomerProduct, ServiceRequest, and Employee entities.",
                    Risk = "High",
                    Recommendation =
                        "Reduce the direct Web to Data dependency and introduce application models or DTOs where appropriate.",
                    Priority = "High"
                },

                new ModernizationFinding
                {
                    Id = "M3.5",
                    Category = "Configuration",
                    Title = "Environment-specific configuration",
                    Description =
                        "Development-oriented and environment-specific settings are directly present in Web.config.",
                    Evidence =
                        "debug=true and LocalDB connection configuration are present in Web.config.",
                    Risk = "Medium",
                    Recommendation =
                        "Introduce environment-aware configuration and externalize environment-specific settings.",
                    Priority = "Medium"
                },

                new ModernizationFinding
                {
                    Id = "M3.6",
                    Category = "Architecture",
                    Title = "UI and business validation coupling",
                    Description =
                        "WebForms code-behind contains validation that overlaps with business-layer validation.",
                    Evidence =
                        "ServiceRequestCreate.aspx.cs validates Customer, Request Type, and Priority while the Business layer independently validates business requirements.",
                    Risk = "Medium",
                    Recommendation =
                        "Keep presentation validation in Web and centralize business invariants in the Business layer.",
                    Priority = "Medium"
                }
            };
        }
    }
}