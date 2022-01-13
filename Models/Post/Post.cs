using System.ComponentModel.DataAnnotations;

namespace BlogAnywhereNET
{
    public class Post
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(150, ErrorMessage = "Post content can only be up to 150 characters")]
        public string Content { get; set; }
        [Required]
        public bool Hidden { get; set; }
        [Required]
        public bool Edited { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        // Relationships
        public int UserId {get; set;}
        public User? User {get; set;}
        public List<Comment>? Comments {get; set;}
        public List<PostLike>? PostLikes {get; set;}
    }
}