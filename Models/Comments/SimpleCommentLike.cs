namespace SocialConnectAPI.Models;

/// <summary>
/// Represents the relationship between a liked comment and a user.
/// </summary>
public class SimpleCommentLike
{
    public int CommentId { get; set; }

    public int UserId { get; set; }
}