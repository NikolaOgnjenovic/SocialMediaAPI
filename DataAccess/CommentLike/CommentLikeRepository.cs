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
        return _databaseContext.CommentLikes.FirstOrDefault(cl => cl.CommentId == commentId && cl.UserId == userId) != null;
    }
}