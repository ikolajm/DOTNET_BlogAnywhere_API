namespace BlogAnywhereNET.Services.PostService
{
    public interface IPostService
    {
        Task<List<Post>> TestGetter();
        Task<Post> CreatePost(Post post);
        Task<Post> EditPost(PostForEdit post);
        Task<object> DeletePost(PostForDelete obj);
        Task<Post> GetPostById(int id);
        Task<object> GetNewsfeed();
        Task<object> GetSinglePost(int id);
    }
}