using System.ComponentModel.DataAnnotations;

namespace BlogAnywhereNET
{
    public class UserRegister
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}