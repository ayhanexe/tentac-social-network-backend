using DomainModels.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<UserPosts> UserPosts { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }  
        public DbSet<ReplyLikes> ReplyLikes { get; set; }
        public DbSet<PostReplies> PostReplies { get; set; }
        public DbSet<UserFriendRequests> UserFirendRequests { get; set; }
        public DbSet<UserFriends> UserFriends { get; set; }
        public DbSet<UserNotification> Notifications { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<UserStories> UserStories { get; set; }
        public DbSet<StoryLikes> StoryLikes { get; set; }
        public DbSet<StoryReplies> StoryReplies { get; set; }
        public DbSet<StoryReplyLikes> StoryReplyLikes { get; set; }
    }
}
