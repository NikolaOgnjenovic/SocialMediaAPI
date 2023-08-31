namespace SocialConnectAPI.DTOs.Comments.Delete.Request;

/// <summary>
/// Represents a request to delete a comment.
/// </summary>
public class DeleteCommentRequest
{
    /// <summary>
    /// The user ID of the user performing the action.
    /// </summary>
    public int UserId { get; set; }
}