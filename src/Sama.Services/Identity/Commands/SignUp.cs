using System;
using Newtonsoft.Json;

namespace Sama.Services.Identity.Commands
{
    public class SignUp : ICommand
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Username { get; }
        public string Password { get; }
        public string Role { get; }

        [JsonConstructor]
        public SignUp(Guid id, string email, string username, string password, string role)
        {
            Id = id;
            Email = email;
            Username = username;
            Password = password;
            Role = role;
        }
    }
}