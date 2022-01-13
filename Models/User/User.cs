using System.ComponentModel.DataAnnotations;

namespace BlogAnywhereNET
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Avatar  { get; set; }

        // Relationships
        // public List<Post> Posts {get; set;}
        // public List<Comment> Comments {get; set;}
        // public List<PostLike> PostLikes {get; set;}
        // public List<CommentLike> CommentLikes {get; set;}
    }
}