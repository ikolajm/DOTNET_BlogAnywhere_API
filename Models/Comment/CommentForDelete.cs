using System.ComponentModel.DataAnnotations;

namespace BlogAnywhereNET
{
    public class CommentForDelete
    {
        public int UserId { get; set; }
        public int CommentId { get; set; }
    }
}