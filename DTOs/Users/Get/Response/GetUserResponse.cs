using SocialConnectAPI.DTOs.Hateoas;
using SocialConnectAPI.Models;

namespace SocialConnectAPI.DTOs.Users.Get.Response;

public class GetUserResponse : LinkCollection
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// The last name of the user.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// The email address of the user. This property is required.
    /// </summary>
    public string Email { get; set; }

    // TODO: Uncomment and implement the FollowingUserIds, LikedPostIds, and LikedCommentIds properties
    // /// <summary>
    // /// The list of IDs of users that this user is following.
    // /// </summary>
    // public List<int> FollowingUserIds { get; set; }
    
    // /// <summary>
    // /// The list of IDs of posts that this user has liked.
    // /// </summary>
    // public List<int> LikedPostIds { get; set; }
    
    // /// <summary>
    // /// The list of IDs of comments that this user has liked.
    // /// </summary>
    // public List<int> LikedCommentIds { get; set; }
    
    /// <summary>
    /// The status of the user (Active / Inactive).
    /// </summary>
    public UserStatus Status { get; set; }
}