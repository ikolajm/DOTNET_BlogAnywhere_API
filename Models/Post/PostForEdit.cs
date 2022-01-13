using System.ComponentModel.DataAnnotations;

namespace BlogAnywhereNET
{
    public class PostForEdit
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public bool Hidden { get; set; }
    }
}