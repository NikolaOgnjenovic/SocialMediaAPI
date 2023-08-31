namespace SocialConnectAPI.DataAccess.CommentLike;

public class CommentLikeRepository : ICommentLikeRepository
{
    private readonly DatabaseContext _databaseContext;

    public CommentLikeRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public bool CommentIsLiked(int commentId, int userId)
    {
        return GetCommentLike(commentId, userId) != null;
    }

    public Models.CommentLike? GetCommentLike(int commentId, int userId)
    {
        return _databaseContext.CommentLikes.FirstOrDefault(cl => cl.CommentId == commentId && cl.UserId == userId);
    }
    
    public void CreateCommentLike(int commentId, int userId)
    {
        _databaseContext.CommentLikes.Add(new Models.CommentLike(commentId, userId));
        _databaseContext.SaveChanges();
    }

    public void DeleteCommentLike(int commentId, int userId)
    {
        var commentLike = GetCommentLike(commentId, userId);
        if (commentLike == null)
        {
            return;
        }
        
        _databaseContext.Remove(commentLike);
        _databaseContext.SaveChanges();
    }
}