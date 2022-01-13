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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IConfiguration _config;
        public CommentController(ICommentService commentService, IConfiguration config)
        {
            _config = config;
            _commentService = commentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Post>> CreateComment(Comment comment )
        {
            // == PRE POST CHECKS == 
            // Make sure no given fields are blank
            if (comment.Content == "")
            {
                return BadRequest("Please ensure your comment has content");
            }
            // == POST POST ==
            var toCreate = await _commentService.CreateComment(comment);

            return Ok(new { comment = toCreate });
        }

        // Edit Comment
        [HttpPut("{id}")]
        public async Task<object> UpdateComment(CommentForEdit commentForEdit)
        {
            var update = await _commentService.EditComment(commentForEdit);

            if (update == "Fail")
            {
                return BadRequest("Could not edit comment");
            }

            return Ok(new { updatedComment = update });
        }

        // Delete Comment
        [HttpDelete("{id}")]
        public async Task<object> DeleteComment(CommentForDelete obj)
        {
            var comment = await _commentService.DeleteComment(obj);

            if (comment == "Fail")
            {
                return BadRequest("Could not delete the comment");
            }

            return Ok(new { message = "Comment deleted" });
        }
    }
}

