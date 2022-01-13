using System.ComponentModel.DataAnnotations;

namespace BlogAnywhereNET
{
    public class LikeForPost
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}