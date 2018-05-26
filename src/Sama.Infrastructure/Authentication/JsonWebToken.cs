using System;

namespace Sama.Infrastructure.Authentication
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long Expires { get; set; }
        public string Role { get; set; }
    }
}