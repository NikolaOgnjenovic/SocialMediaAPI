namespace SocialConnectAPI.DataAccess.PostLike;

public interface IPostLikeRepository
{
    public bool PostIsLiked(int postId, int userId);
    public Models.PostLike? GetPostLike(int commentId, int userId);
    public void CreatePostLike(int commentId, int userId);
    public void DeletePostLike(int commentId, int userId);
}