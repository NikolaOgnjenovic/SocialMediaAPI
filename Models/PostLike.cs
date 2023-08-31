namespace SocialConnectAPI.Models;

/// <summary>
/// Represents the relationship between a liked post and a user.
/// </summary>
public class PostLike
{
    /// <summary>
    /// The unique identifier of the post that the user liked.
    /// </summary>
    public int PostId { get; set; }
    public Post Post { get; set; }
    
    /// <summary>
    /// The unique identifier of the user that liked the post.
    /// </summary>
    public int UserId { get; set; }
    public User User { get; set; }
}