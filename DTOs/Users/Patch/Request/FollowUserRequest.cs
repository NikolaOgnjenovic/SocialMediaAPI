namespace SocialConnectAPI.DTOs.Users.Patch.Request;

/// <summary>
/// Represents a request to follow or unfollow a user.
/// </summary>
public class FollowUserRequest
{
    /// <summary>
    /// The user ID of the user following or unfollowing.
    /// </summary>
    public int FollowerId { get; set; }
    
    /// <summary>
    /// The user ID of the user being followed or unfollowed.
    /// </summary>
    public int FollowedId { get; set; }
}