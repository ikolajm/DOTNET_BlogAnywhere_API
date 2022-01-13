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
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        private readonly IConfiguration _config;
        public LikeController(ILikeService likeService, IConfiguration config)
        {
            _config = config;
            _likeService = likeService;
        }

        // Like post
        [HttpPost("post")]
        [Authorize]
        public async Task<ActionResult<object>> CreatePostLike(LikeForPost like)
        {
            var likedPost = await _likeService.CreatePostLike(like);
            if(likedPost == "Fail")
            {
                return BadRequest("Could not like the post");
            }

            return Ok(likedPost);
        }
        // Unlike post
        [HttpDelete("post")]
        [Authorize]
        public async Task<ActionResult<object>> DeletePostLike(LikeForDelete like)
        {
            var deletedLike = await _likeService.DeletePostLike(like);

            if (deletedLike == "Fail")
            {
                return BadRequest("Could not delete the like");
            }

            return Ok(new { message = "Like deleted" });
        }
        
        // Like comment
        [HttpPost("comment")]
        [Authorize]
        public async Task<ActionResult<object>> CreateCommentLike(LikeForComment like)
        {
            var likedComment = await _likeService.CreateCommentLike(like);
            if(likedComment == "Fail")
            {
                return BadRequest("Could not like the post");
            }

            return Ok(likedComment);
        }
        // Unlike comment
        [HttpDelete("comment")]
        [Authorize]
        public async Task<ActionResult<object>> DeleteCommentLike(LikeForDelete like)
        {
            var deletedLike = await _likeService.DeleteCommentLike(like);

            if (deletedLike == "Fail")
            {
                return BadRequest("Could not delete the like");
            }

            return Ok(new { message = "Like deleted" });
        }
    }
}

