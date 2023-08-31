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
    
    public Models.PostLike? GetPostLike(int postId, int userId)
    {
        return _databaseContext.PostLikes.FirstOrDefault(cl => cl.PostId == postId && cl.UserId == userId);
    }
    
    public void CreatePostLike(int postId, int userId)
    {
        _databaseContext.PostLikes.Add(new Models.PostLike(postId, userId));
        _databaseContext.SaveChanges();
    }

    public void DeletePostLike(int postId, int userId)
    {
        var postLike = GetPostLike(postId, userId);
        if (postLike == null)
        {
            return;
        }
        
        _databaseContext.Remove(postLike);
        _databaseContext.SaveChanges();
    }
}