using BlogAnywhereNET.Models;
using BlogAnywhereNET.Services.AuthService;
using Microsoft.EntityFrameworkCore;

namespace BlogAnywhereNET.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly DbSetup _context;
        private readonly IAuthService _authService;
        public PostService(DbSetup context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<List<Post>> TestGetter()
        {
            var posts = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments).ThenInclude(c => c.CommentLikes)
                .Include(p => p.PostLikes).ThenInclude(pl => pl.User)
                .ToListAsync();
            
            return posts;
        }

        public async Task<Post> CreatePost(Post post)
        {
            var postAuthor = await _authService.GetUserById(post.UserId);

            var toCreate = new Post();
            toCreate.Content = post.Content;
            toCreate.Hidden = false;
            toCreate.Edited = false;
            toCreate.CreatedAt = DateTime.UtcNow;
            // RELATIONSHIP DATA
            // User - author
            toCreate.UserId = post.UserId;

            _context.Posts.Add(toCreate);
            _context.SaveChanges();

            var newPost = await GetPostById(toCreate.Id);
            return newPost;
        }

        public async Task<Post> EditPost(PostForEdit post)
        {
            var existingPost = await _context.Posts.FirstOrDefaultAsync(x => x.Id == post.PostId);
            existingPost.Content = post.Content;
            existingPost.Hidden = post.Hidden;
            existingPost.Edited = true;
            _context.SaveChanges();

            var newPost = await GetPostById(existingPost.Id);
            return newPost;
        }

        public async Task<object> DeletePost(PostForDelete obj)
        {
            var post = await _context.Posts
                .Where(p => p.Id == obj.PostId)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                return "Fail";
            }
            else
            {
                if (post.UserId != obj.UserId)
                {
                    return "Fail";
                }
                else
                {
                    _context.Posts.Remove(post);
                    await _context.SaveChangesAsync();

                    return "Success";
                }
            }
        }

        public async Task<Post> GetPostById(int id)
        {
            var post = await _context.Posts
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return post;
        }

        // Get newsfeed view
        public async Task<object> GetNewsfeed()
        {
            var newsfeed = await _context.Posts
                // Post Author
                .Include(p => p.User)
                // Post Likes
                .Include(p => p.PostLikes)
                    // Like User
                    .ThenInclude(pl => pl.User)
                // Post Comments
                .Include(p => p.Comments)
                    // Comment Likes
                    .ThenInclude(c => c.CommentLikes)
                    // Like Users
                    .ThenInclude(cl => cl.User)
                .ToListAsync();
            
            return newsfeed;
        }

        // Get post for singular view
        public async Task<object> GetSinglePost(int Id)
        {
            var newsfeed = await _context.Posts
                .Where(x => x.Id == Id)
                // Post Author
                .Include(p => p.User)
                // Post Likes
                .Include(p => p.PostLikes)
                    // Like User
                    .ThenInclude(pl => pl.User)
                // Post Comments
                .Include(p => p.Comments)
                    // Comment Likes
                    .ThenInclude(c => c.CommentLikes)
                    // Like Users
                    .ThenInclude(cl => cl.User)
                .ToListAsync();
            
            return newsfeed;
        }

    }
}