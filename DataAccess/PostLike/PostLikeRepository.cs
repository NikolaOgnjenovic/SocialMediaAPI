namespace SocialConnectAPI.DataAccess.PostLike;

public class PostLikeRepository : IPostLikeRepository
{
    private readonly DatabaseContext _databaseContext;

    public PostLikeRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public bool PostIsLiked(int postId, int userId)
    {
        return _databaseContext.PostLikes.FirstOrDefault(pl => pl.PostId == postId && pl.UserId == userId) != null;
    }
}