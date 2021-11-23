namespace TTGS.Shared.Helper
{
    public static class ConditionHelper
    {
        public static bool ValidatePassword(string password)
        {
            return password.Length > 6;
        }
    }
}
