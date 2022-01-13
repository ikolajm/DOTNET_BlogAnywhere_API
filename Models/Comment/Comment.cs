using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogAnywhereNET
{
    public class Comment
    {
        [Required]
        public int Id { get; set; }
        public string Content { get; set; }
        [Required]
        public bool Hidden { get; set; }
        [Required]
        public bool Edited { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        // Relationships
        public int PostId {get; set;}
        public int UserId {get; set;}
        public User? User {get; set;}
        public List<CommentLike>? CommentLikes {get; set;}
    }
}