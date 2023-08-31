using Microsoft.EntityFrameworkCore;
using SocialConnectAPI.Models;

namespace SocialConnectAPI.DataAccess;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Models.PostLike> PostLikes { get; set; }
    public DbSet<Models.CommentLike> CommentLikes { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        // Configure many-to-many relationship between User and Post
        modelBuilder.Entity<Models.PostLike>()
            .HasKey(ul => new { ul.UserId, ul.PostId });

        modelBuilder.Entity<Models.PostLike>()
            .HasOne(ul => ul.User)
            .WithMany(u => u.PostLikes)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Set cascade behavior to restrict

        modelBuilder.Entity<Models.PostLike>()
            .HasOne(ul => ul.Post)
            .WithMany(p => p.UsersWhoLiked)
            .HasForeignKey(ul => ul.PostId)
            .OnDelete(DeleteBehavior.Restrict); // Set cascade behavior to restrict
        
        // Configure many-to-many relationship between User and Comment
        modelBuilder.Entity<Models.CommentLike>()
            .HasKey(ul => new { ul.UserId, ul.CommentId });

        modelBuilder.Entity<Models.CommentLike>()
            .HasOne(ul => ul.User)
            .WithMany(u => u.CommentLikes)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Set cascade behavior to restrict

        modelBuilder.Entity<Models.CommentLike>()
            .HasOne(ul => ul.Comment)
            .WithMany(c => c.UsersWhoLiked)
            .HasForeignKey(ul => ul.CommentId)
            .OnDelete(DeleteBehavior.Restrict); // Set cascade behavior to restrict
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Followers)
            .WithMany(u => u.Following)
            .UsingEntity(j => j.ToTable("UserFollowers"));

        base.OnModelCreating(modelBuilder);
    }
}