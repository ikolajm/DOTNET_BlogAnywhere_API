using BlogAnywhereNET.Models;
using BlogAnywhereNET.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogAnywhereNET.Services.PostService
{
    public class LikeService : ILikeService
    {
        private readonly DbSetup _context;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        public LikeService(DbSetup context, IPostService postService, ICommentService commentService)
        {
            _context = context;
            _postService = postService;
            _commentService = commentService;
        }

        // Like Post
        public async Task<object> CreatePostLike(LikeForPost post)
        {
            // Check if the user has already liked the post
            var foundLike = await DoesPostLikeExist(post);
            if (foundLike)
            {
                return "Fail";
            }

            var foundPost = await _postService.GetPostById(post.PostId);
            // return foundPost;

            var toCreate = new PostLike();
            toCreate.CreatedAt = DateTime.UtcNow;
            // RELATIONSHIP DATA
            // User - author
            toCreate.UserId = post.UserId;
            toCreate.PostId = post.PostId;

            // Create the postLike
            _context.PostLikes.Add(toCreate);
            _context.SaveChanges();

            // var newLikedPost = await _postService.GetPostById(toCreate.PostId);
            return new { like = toCreate };
        }
        // Unlike Post
        public async Task<object> DeletePostLike(LikeForDelete like)
        {
            var foundLike = await _context.PostLikes
                .Where(p => p.Id == like.LikeId)
                .FirstOrDefaultAsync();

            if (foundLike == null)
            {
                return "Fail";
            }
            else
            {
                if (foundLike.UserId != like.UserId)
                {
                    return "Fail";
                }
                else
                {
                    _context.PostLikes.Remove(foundLike);
                    await _context.SaveChangesAsync();

                    return "Success";
                }
            }
        }

        // Like Comment
        public async Task<object> CreateCommentLike(LikeForComment comment)
        {
            // Check if the user has already liked the post
            var foundLike = await DoesCommentLikeExist(comment);
            if (foundLike)
            {
                return "Fail";
            }

            var foundComment = await _commentService.GetCommentById(comment.CommentId);
            // return foundPost;

            var toCreate = new CommentLike();
            toCreate.CreatedAt = DateTime.UtcNow;
            // RELATIONSHIP DATA
            // User - author
            toCreate.UserId = comment.UserId;
            toCreate.CommentId = comment.CommentId;

            // Create the postLike
            _context.CommentLikes.Add(toCreate);
            _context.SaveChanges();

            return new { like = toCreate };
        }
        // Unlike Comment
        public async Task<object> DeleteCommentLike(LikeForDelete obj)
        {
            var foundLike = await _context.CommentLikes
                .Where(l => l.Id == obj.LikeId)
                .FirstOrDefaultAsync();

            if (foundLike == null)
            {
                return "Fail";
            }
            else
            {
                if (foundLike.UserId != obj.UserId)
                {
                    return "Fail";
                }
                else
                {
                    _context.CommentLikes.Remove(foundLike);
                    await _context.SaveChangesAsync();

                    return "Success";
                }
            }
        }

        // Does postlike exist
        public async Task<bool> DoesPostLikeExist(LikeForPost post)
        {
            if (await _context.PostLikes.AnyAsync(x => x.UserId == post.UserId && x.PostId == post.PostId))
            {
                return true;
            } else {
                return false;
            }
        }

        public async Task<bool> DoesCommentLikeExist(LikeForComment comment)
        {
            if (await _context.CommentLikes.AnyAsync(x => x.UserId == comment.UserId && x.CommentId == comment.CommentId))
            {
                return true;
            } else {
                return false;
            }
        }
    }
}