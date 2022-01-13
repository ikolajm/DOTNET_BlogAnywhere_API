namespace BlogAnywhereNET.Services.PostService
{
    public interface ILikeService
    {
        Task<object> CreatePostLike(LikeForPost like);
        Task<object> DeletePostLike(LikeForDelete like);
        Task<object> CreateCommentLike(LikeForComment like);
        Task<object> DeleteCommentLike(LikeForDelete like);
    }
}