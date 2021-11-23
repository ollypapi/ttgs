namespace TTGS.Shared.Constants
{
    public static class UserRoleConstants
    {
        public const string Admin = "Admin";
        public const string Client = "Client";
        public const string Contractor = "Contractor";
        public const string Fleet = "Fleet";
        public const string Employee = "Employee";
        public const string Expenses = "Expenses";

        public static string[] AllowedRoles = new[] { Client, Contractor, Fleet, Employee, Expenses };
    }
}
