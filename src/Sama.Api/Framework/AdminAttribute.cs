namespace Sama.Api.Framework
{
    public class AdminAttribute : AuthAttribute
    {
        public AdminAttribute() : base("admin")
        {
        }
    }
}