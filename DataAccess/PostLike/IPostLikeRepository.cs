namespace SocialConnectAPI.DataAccess.PostLike;

public interface IPostLikeRepository
{
    public bool PostIsLiked(int postId, int userId);
}