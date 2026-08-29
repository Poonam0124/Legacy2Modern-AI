namespace Legacy2Modern.Business.Services
{
    public static class LegacyWorkflowRules
    {
        public static bool IsValidTransition(
            string currentStatus,
            string newStatus)
        {
            if (string.IsNullOrWhiteSpace(currentStatus) ||
                string.IsNullOrWhiteSpace(newStatus))
            {
                return false;
            }

            currentStatus =
                currentStatus.Trim();

            newStatus =
                newStatus.Trim();

            if (currentStatus.Equals(
                    "Open",
                    System.StringComparison.OrdinalIgnoreCase))
            {
                if (newStatus.Equals(
                        "Assigned",
                        System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            if (currentStatus.Equals(
                    "Assigned",
                    System.StringComparison.OrdinalIgnoreCase))
            {
                if (newStatus.Equals(
                        "In Progress",
                        System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            if (currentStatus.Equals(
                    "In Progress",
                    System.StringComparison.OrdinalIgnoreCase))
            {
                if (newStatus.Equals(
                        "Resolved",
                        System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            if (currentStatus.Equals(
                    "Resolved",
                    System.StringComparison.OrdinalIgnoreCase))
            {
                if (newStatus.Equals(
                        "Closed",
                        System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}