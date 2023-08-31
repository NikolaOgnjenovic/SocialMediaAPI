using SocialConnectAPI.Models;

namespace SocialConnectAPI.DataAccess.Comments;

public interface ICommentRepository
{
    Comment? GetCommentById(int commentId);
    Comment? GetActiveCommentById(int commentId);
    List<Comment> GetCommentsByUserId(int userId);
    List<Comment> GetActiveCommentsByUserId(int userId);
    Comment CreateComment(Comment comment);
    Comment? UpdateComment(Comment comment);
    Comment? DeleteComment(int commentId);
    Comment? LikeComment(int commentId);
    Comment? DislikeComment(int commentId);
    void SetInactive(int userId);
    void SetActive(int userId);
    void ArchiveByPostId(int postId);
}