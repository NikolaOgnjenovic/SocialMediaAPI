using SocialConnectAPI.Models;

namespace SocialConnectAPI.DataAccess.Posts;

public class PostRepository : IPostRepository
{
    private readonly DatabaseContext _databaseContext;

    public PostRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public Post? GetPostById(int postId)
    {
        return _databaseContext.Posts.FirstOrDefault(post => post.Id == postId);
    }

    public List<Post> GetPostsByUserId(int userId)
    {
        return _databaseContext.Posts.ToList().FindAll(post => post.AuthorId == userId);
    }
    
    public List<Post> GetPostsByTag(string tag)
    {
        throw new NotImplementedException("Tag search not implemented");
        //return _databaseContext.Posts.ToList().FindAll(post => post.AuthorId == userId);
    }

    public Post CreatePost(Post post)
    {
        var createdPost = _databaseContext.Posts.Add(post);
        _databaseContext.SaveChanges();
        return createdPost.Entity;
    }

    public Post? UpdatePost(Post post)
    {
        var postInDatabase = GetPostById(post.Id);
        if (postInDatabase == null)
        {
            return null;
        }

        postInDatabase = post;
        _databaseContext.SaveChanges();
        return postInDatabase;
    }

    public Post? DeletePost(int postId)
    {
        var postInDatabase = GetPostById(postId);
        if (postInDatabase == null)
        {
            return null;
        }
        
        var deletedPost = _databaseContext.Posts.Remove(postInDatabase);
        _databaseContext.SaveChanges();
        return deletedPost.Entity;
    }

    public Post? ArchivePost(int postId)
    {
        var postInDatabase = GetPostById(postId);
        if (postInDatabase == null)
        {
            return null;
        }

        postInDatabase.Status = PostStatus.Archived;
        _databaseContext.SaveChanges();
        return postInDatabase;
    }
    
    public Post? LikePost(int postId)
    {
        var postInDatabase = GetPostById(postId);
        if (postInDatabase == null)
        {
            return null;
        }

        postInDatabase.LikeCount += 1;
        _databaseContext.SaveChanges();
        return postInDatabase;
    }

    public Post? DislikePost(int postId)
    {
        var postInDatabase = GetPostById(postId);
        if (postInDatabase == null)
        {
            return null;
        }

        postInDatabase.LikeCount -= 1;
        _databaseContext.SaveChanges();
        return postInDatabase;
    }

    public void SetInactive(int userId)
    {
        var postsByUser = GetPostsByUserId(userId);
        foreach (var post in postsByUser)
        {
            post.Status = PostStatus.UserInactive;
        }
    }
    
    public void SetActive(int userId)
    {
        var postsByUser = GetPostsByUserId(userId);
        foreach (var post in postsByUser)
        {
            post.Status = PostStatus.Active;
        }
    }
}