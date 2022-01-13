using System.ComponentModel.DataAnnotations;

namespace BlogAnywhereNET
{
    public class UserForEdit
    {
        public int  UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
    }
}