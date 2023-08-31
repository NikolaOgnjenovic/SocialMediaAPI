using SocialConnectAPI.Models;

namespace SocialConnectAPI.DataAccess.Comments;

public class CommentRepository : ICommentRepository
{
    private readonly DatabaseContext _databaseContext;

    public CommentRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public Comment? GetCommentById(int commentId)
    {
        return _databaseContext.Comments.FirstOrDefault(comment => comment.Id == commentId);
    }
    
    public Comment? GetActiveCommentById(int commentId)
    {
        return _databaseContext.Comments.FirstOrDefault(comment => comment.Id == commentId && comment.Status == CommentStatus.Active);
    }

    public List<Comment> GetCommentsByUserId(int userId)
    {
        return _databaseContext.Comments.ToList().FindAll(comment => comment.AuthorId == userId);
    }
    
    public List<Comment> GetActiveCommentsByUserId(int userId)
    {
        return _databaseContext.Comments.ToList().FindAll(comment => comment.AuthorId == userId && comment.Status == CommentStatus.Active);
    }

    public Comment CreateComment(Comment comment)
    {
        var createdComment = _databaseContext.Comments.Add(comment);
        _databaseContext.SaveChanges();
        return createdComment.Entity;
    }

    public Comment? UpdateComment(Comment comment)
    {
        var commentInDatabase = GetCommentById(comment.Id);
        if (commentInDatabase == null)
        {
            return null;
        }

        commentInDatabase = comment;
        _databaseContext.SaveChanges();
        return commentInDatabase;
    }

    public Comment? DeleteComment(int commentId)
    {
        var commentInDatabase = GetCommentById(commentId);
        if (commentInDatabase == null)
        {
            return null;
        }
        
        var deletedComment = _databaseContext.Comments.Remove(commentInDatabase);
        _databaseContext.SaveChanges();
        return deletedComment.Entity;
    }

    public Comment? LikeComment(int commentId)
    {
        var commentInDatabase = GetActiveCommentById(commentId);
        if (commentInDatabase == null)
        {
            return null;
        }

        commentInDatabase.LikeCount += 1;
        _databaseContext.SaveChanges();
        return commentInDatabase;
    }

    public Comment? DislikeComment(int commentId)
    {
        var commentInDatabase = GetActiveCommentById(commentId);
        if (commentInDatabase == null)
        {
            return null;
        }

        commentInDatabase.LikeCount -= 1;
        _databaseContext.SaveChanges();
        return commentInDatabase;
    }

    public void SetInactive(int userId)
    {
        var commentsByUser = GetCommentsByUserId(userId);
        foreach (var comment in commentsByUser)
        {
            comment.Status = CommentStatus.UserInactive;
        }

        _databaseContext.SaveChanges();
    }
    
    public void SetActive(int userId)
    {
        var commentsByUser = GetCommentsByUserId(userId);
        foreach (var comment in commentsByUser)
        {
            comment.Status = CommentStatus.Active;
        }
        
        _databaseContext.SaveChanges();
    }

    public void ArchiveByPostId(int postId)
    {
        var commentsByPostId = _databaseContext.Comments.ToList().FindAll(comment => comment.PostId == postId);
        foreach (var comment in commentsByPostId)
        {
            comment.Status = CommentStatus.PostArchived;
        }
        _databaseContext.SaveChanges();
    }
}