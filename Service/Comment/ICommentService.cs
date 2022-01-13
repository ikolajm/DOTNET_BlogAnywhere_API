namespace BlogAnywhereNET.Services.PostService
{
    public interface ICommentService
    {
        Task<Comment> CreateComment(Comment comment);
        Task<object> EditComment(CommentForEdit comment);
        Task<object> DeleteComment(CommentForDelete comment);
        Task<Comment> GetCommentById(int id);
    }
}