using System.Collections.Generic;
using Legacy2Modern.Business.Models.AI;

namespace Legacy2Modern.Business.Services.AI
{
    public class MockModernizationPlanningAIProvider
        : IAIPlanningProvider
    {
        public ModernizationPlanningResponse CreatePlan(
            ModernizationPlanningRequest request)
        {
            return new ModernizationPlanningResponse
            {
                ProviderName = "Mock",

                Plan = new ModernizationPlan
                {
                    PlanTitle =
                        "Legacy2Modern-AI Modernization Plan",

                    OverallStrategy =
                        "Modernize incrementally by addressing high-risk legacy concerns first while preserving existing business functionality.",

                    TargetArchitecture =
                        "ASP.NET Core Web API, modern frontend, SQL Server, and incremental migration from the existing Web Forms application.",

                    Phases =
                        new List<ModernizationPlanPhase>
                        {
                            new ModernizationPlanPhase
                            {
                                PhaseNumber = 1,
                                PhaseName =
                                    "Foundation and Risk Reduction",
                                Objective =
                                    "Reduce architectural and configuration risks before introducing new application components.",
                                Rationale =
                                    "Establishing a safer foundation reduces the risk of carrying legacy problems into the modern architecture."
                            },

                            new ModernizationPlanPhase
                            {
                                PhaseNumber = 2,
                                PhaseName =
                                    "Application Layer Modernization",
                                Objective =
                                    "Introduce clearer application boundaries and modern service interfaces.",
                                Rationale =
                                    "Separating business capabilities from Web Forms dependencies enables incremental migration."
                            },

                            new ModernizationPlanPhase
                            {
                                PhaseNumber = 3,
                                PhaseName =
                                    "Modern Platform Migration",
                                Objective =
                                    "Move selected capabilities toward the target architecture.",
                                Rationale =
                                    "Incremental migration reduces business disruption compared with a complete rewrite."
                            }
                        },

                    Items =
                        new List<ModernizationPlanItem>
                        {
                            new ModernizationPlanItem
                            {
                                PlanItemId = "PLAN-001",
                                PhaseNumber = 1,
                                FindingId = "F001",
                                Title =
                                    "Centralize workflow rules",
                                RecommendedAction =
                                    "Move hard-coded workflow rules into a dedicated application/service layer.",
                                Reasoning =
                                    "Centralizing rules improves maintainability and reduces duplicated behavior.",
                                Priority = "High",
                                Risk = "High",
                                Complexity = "Medium",
                                Effort = "Medium",
                                Dependencies =
                                    new List<string>(),
                                AffectedComponents =
                                    new List<string>
                                    {
                                        "Legacy2Modern.Web",
                                        "Legacy2Modern.Business"
                                    },
                                ImplementationSteps =
                                    new List<string>
                                    {
                                        "Identify existing workflow rules.",
                                        "Create centralized workflow services.",
                                        "Replace duplicated UI logic.",
                                        "Validate existing workflows."
                                    }
                            }
                        }
                }
            };
        }
    }
}