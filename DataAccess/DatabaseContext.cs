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
    //public DbSet<Followers> Followers { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }
    public DbSet<CommentLike> CommentLikes { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        // Configure many-to-many relationship between User and Post
        modelBuilder.Entity<PostLike>()
            .HasKey(ul => new { ul.UserId, ul.PostId });

        modelBuilder.Entity<PostLike>()
            .HasOne(ul => ul.User)
            .WithMany(u => u.PostLikes)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Set cascade behavior to restrict

        modelBuilder.Entity<PostLike>()
            .HasOne(ul => ul.Post)
            .WithMany(p => p.UsersWhoLiked)
            .HasForeignKey(ul => ul.PostId)
            .OnDelete(DeleteBehavior.Restrict); // Set cascade behavior to restrict
        
        // Configure many-to-many relationship between User and Comment
        modelBuilder.Entity<CommentLike>()
            .HasKey(ul => new { ul.UserId, ul.CommentId });

        modelBuilder.Entity<CommentLike>()
            .HasOne(ul => ul.User)
            .WithMany(u => u.CommentLikes)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Set cascade behavior to restrict

        modelBuilder.Entity<CommentLike>()
            .HasOne(ul => ul.Comment)
            .WithMany(c => c.UsersWhoLiked)
            .HasForeignKey(ul => ul.CommentId)
            .OnDelete(DeleteBehavior.Restrict); // Set cascade behavior to restrict

        base.OnModelCreating(modelBuilder);
    }
}