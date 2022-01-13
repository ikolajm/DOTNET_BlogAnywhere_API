using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogAnywhereNET
{
    public class CommentLike
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        // Relationships
        public int CommentId {get; set;}
        [JsonIgnore]
        public Comment? Comment {get; set;}
        public int UserId {get; set;}
        public User? User {get; set;}
    }
}