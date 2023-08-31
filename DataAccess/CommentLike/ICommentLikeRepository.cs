namespace SocialConnectAPI.DataAccess.CommentLike;

public interface ICommentLikeRepository
{
    public bool CommentIsLiked(int commentId, int userId);
}