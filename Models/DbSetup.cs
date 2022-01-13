using Microsoft.EntityFrameworkCore;

namespace BlogAnywhereNET.Models
{
    public class DbSetup : DbContext
    {
        public DbSetup(DbContextOptions<DbSetup> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }

    }
}