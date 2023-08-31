namespace SocialConnectAPI.DTOs.Comments.Patch.Request;

/// <summary>
/// Represents a request to like a comment.
/// </summary>
public class LikeCommentRequest
{
    /// <summary>
    /// The user ID of the user performing the action.
    /// </summary>
    public int UserId { get; set; }
}