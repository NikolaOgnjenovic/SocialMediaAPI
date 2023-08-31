namespace SocialConnectAPI.DataAccess.CommentLike;

public interface ICommentLikeRepository
{
    public bool CommentIsLiked(int commentId, int userId);

    public Models.CommentLike? GetCommentLike(int commentId, int userId);
    public void CreateCommentLike(int commentId, int userId);
    public void DeleteCommentLike(int commentId, int userId);
}