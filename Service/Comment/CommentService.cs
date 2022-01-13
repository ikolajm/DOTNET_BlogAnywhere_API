using BlogAnywhereNET.Models;
using BlogAnywhereNET.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogAnywhereNET.Services.PostService
{
    public class CommentService : ICommentService
    {
        private readonly DbSetup _context;
        private readonly IPostService _postService;
        private readonly IAuthService _authService;
        public CommentService(DbSetup context, IPostService postService, IAuthService authService)
        {
            _context = context;
            _postService = postService;
            _authService = authService;
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
            var toCreate = new Comment();
            toCreate.Content = comment.Content;
            toCreate.Hidden = false;
            toCreate.Edited = false;
            toCreate.CreatedAt = DateTime.UtcNow;
            // RELATIONSHIP DATA
            // User - author
            toCreate.UserId = comment.UserId;
            // Post - parent model
            toCreate.PostId = comment.PostId;

            _context.Comments.Add(toCreate);
            _context.SaveChanges();

            return toCreate;
        }
        public async Task<object> EditComment(CommentForEdit comment)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == comment.CommentId);
            if (existingComment == null || existingComment.UserId != comment.UserId)
            {
                return "Fail";
            }
            existingComment.Content = comment.Content;
            existingComment.Hidden = comment.Hidden;
            existingComment.Edited = true;
            _context.SaveChanges();

            return existingComment;
        }

        public async Task<object> DeleteComment(CommentForDelete obj)
        {
            var comment = await _context.Comments
                .Where(p => p.Id == obj.CommentId)
                .FirstOrDefaultAsync();

            if (comment == null)
            {
                return "Fail";
            }
            else
            {
                if (comment.UserId != obj.UserId)
                {
                    return "Fail";
                }
                else
                {
                    _context.Comments.Remove(comment);
                    await _context.SaveChangesAsync();

                    return "Success";
                }
            }
        }

        public async Task<Comment> GetCommentById(int id)
        {
            var comment = await _context.Comments
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return comment;
        }
    }

}