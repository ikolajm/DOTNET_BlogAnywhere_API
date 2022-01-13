using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BlogAnywhereNET.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using BlogAnywhereNET.Services.PostService;

namespace BlogAnywhereNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IConfiguration _config;
        public PostController(IPostService postService, IConfiguration config)
        {
            _config = config;
            _postService = postService;
        }

        [HttpGet("test")]
        [Authorize]
        public async Task<ActionResult<Post[]>> Test()
        {
            var test = await _postService.TestGetter();

            return Ok(new { posts = test });
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            // == PRE POST CHECKS == 
            // Make sure no given fields are blank
            if (post.Content == "")
            {
                return BadRequest("Please ensure your post has content");
            }
            // == POST POST ==
            var toCreate = await _postService.CreatePost(post);

            return Ok(new { post = toCreate });
        }

        // Edit Post
        [HttpPut("{id}")]
        public async Task<object> UpdatePost(PostForEdit postForEdit)
        {
            var update = await _postService.EditPost(postForEdit);

            return Ok(new { updatedPost = update });
        }

        // Delete Post
        [HttpDelete("{id}")]
        public async Task<object> DeletePost(PostForDelete obj)
        {
            var post = await _postService.DeletePost(obj);

            if (post == "Fail")
            {
                return BadRequest("Could not delete the post");
            }

            return Ok(new { message = "Post deleted" });
        }

        // Get all posts for newsfeed view (post, author, like count, liked, participant count, participated)
        [Authorize]
        [HttpGet("newsfeed")]
        public async Task<object> GetNewsfeed()
        {
            var newsfeed = await _postService.GetNewsfeed();

            if (newsfeed == "Fail")
            {
                return BadRequest("Could not fetch the newsfeed");
            }

            return Ok(newsfeed);
        }

        // Get a single post for newsfeed view (post, author, like count, liked, participants count, participated, all comments with authors and likes)
        [Authorize]
        [HttpGet("{id}")]
        public async Task<object> GetSinglePost(int id)
        {
            var post = await _postService.GetSinglePost(id);

            if (post == "Fail")
            {
                return BadRequest("Could not fetch the post");
            }

            return Ok(post);
        }
    }
}

