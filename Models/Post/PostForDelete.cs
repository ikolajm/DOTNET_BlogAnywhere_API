using System.ComponentModel.DataAnnotations;

namespace BlogAnywhereNET
{
    public class PostForDelete
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}