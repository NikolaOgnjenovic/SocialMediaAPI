namespace SocialConnectAPI.DTOs.Posts.Patch.Request;

/// <summary>
/// Represents a request to like a post.
/// </summary>
public class LikePostRequest
{
    /// <summary>
    /// The user ID of the user performing the action.
    /// </summary>
    public int UserId { get; set; }
}