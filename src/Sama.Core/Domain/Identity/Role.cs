namespace Sama.Core.Domain.Identity
{
    public static class Role
    {
        public const string User = "user";
        public const string Ngo = "ngo";
        public const string Admin = "admin";

        public static bool IsValid(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return false;
            }
            role = role.ToLowerInvariant();

            return role == User || role == Admin || role == Ngo;
        }
    }
}