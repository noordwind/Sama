namespace Sama.Api.Framework
{
    public class AdminAuthAttribute : AuthAttribute
    {
        public AdminAuthAttribute() : base("admin")
        {
        }
    }
}