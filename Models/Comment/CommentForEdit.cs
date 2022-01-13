using System.ComponentModel.DataAnnotations;

namespace BlogAnywhereNET
{
    public class CommentForEdit
    {
        [Required]
        public int CommentId { get; set; }
        public string Content { get; set; }
        public bool Hidden { get; set; }
        public int UserId { get; set; }
    }
}