namespace SocialConnectAPI.Models;

/// <summary>
/// Represents the relationship between a liked comment and a user.
/// </summary>
public class CommentLike
{
    public int CommentId { get; set; }
    public Comment Comment { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } // Navigation property to User

    public CommentLike(int commentId, int userId)
    {
        CommentId = commentId;
        UserId = userId;
    }
}