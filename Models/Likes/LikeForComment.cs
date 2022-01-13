using System.ComponentModel.DataAnnotations;

namespace BlogAnywhereNET
{
    public class LikeForComment
    {
        public int UserId { get; set; }
        public int CommentId { get; set; }
    }
}