using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogAnywhereNET
{
    public class PostLike
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        // Relationships
        // [JsonIgnore]
        public int PostId {get; set;}
        [JsonIgnore]
        public Post? Post {get; set;}
        public int UserId {get; set;}
        public User? User {get; set;}
    }
}